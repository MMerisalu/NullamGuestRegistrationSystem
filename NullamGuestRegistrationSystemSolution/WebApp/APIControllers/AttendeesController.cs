using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using App.Contracts.DAL;
using App.DAL.DTO;
using App.Enum;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public AttendeesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Attendees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendeeDTO?>>>? GetAttendees()
        {
            return Ok(await _uow.Attendees.GetAllAttendeesOrderedByNameAsync());
        }

        // GET: api/Attendees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendeeDTO?>>? GetAttendee(int id)
        {
            var attendee = await _uow.Attendees.GetAttendeeByIdAsync(id);

            if (attendee == null)
            {
                return NotFound();
            }

            return attendee;
        }

        // PUT: api/Attendees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{eventId}")]

        public async Task<IActionResult> PutAttendee(int id, AttendeeDTO attendee, [FromRoute] int eventId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != attendee.Id)
            {
                return BadRequest();
            }
            var attendeeDb = await _uow.Attendees.GetAttendeeByIdAsync(id, noIncludes: true);
            if (attendeeDb == null)
            {
                return BadRequest("Kirjet ei leitud!");
            }
            var eventDb = await _uow.Events.GetEventByIdAsync(eventId, noIncludes: true);
            if (eventDb == null)
            {
                return BadRequest("Kirjet ei leitud!");
            }
            if (eventDb.EventDateAndTime < DateTime.Now)
            {
                return BadRequest("Kirjet ei leitud!");
            }
            var eventAndAttendeDb = await _uow.EventsAndAttendes.GetEventAndAttendeeDTOAsync(eventId, id);
            if (eventAndAttendeDb == null)
            {
                return BadRequest("Kirjet ei leitud!");
            }
            try
            {

                if (attendeeDb.AttendeeType.HasValue)
                {
                    if (attendeeDb.AttendeeType == AttendeeType.Person)
                    {
                        attendeeDb.SurName = attendee.SurName;
                        attendeeDb.GivenName = attendee.GivenName;
                        // If there is no change to PersonalIdentifier, then just change their name globally
                        if (attendee.PersonalIdentifier != null && !attendeeDb.PersonalIdentifier.Equals(attendee.PersonalIdentifier))
                        {
                            attendeeDb.PersonalIdentifier = attendee.PersonalIdentifier;
                            var isRegistered = await _uow.Attendees.IsAttendeeAlreadyRegisteredAsync(attendeeDb.AttendeeType.Value, attendee.PersonalIdentifier, null, null, true, true);
                            if (isRegistered.Value == true)
                            {
                                return BadRequest("Sisestatud isikukoodiga eraisik on juba registeeritud!");
                            }
                        }

                        if (attendee.PersonAdditionalInfo != null)
                        {
                            attendeeDb.PersonAdditionalInfo = attendee.PersonAdditionalInfo;
                        }


                        if (attendee.PaymentMethodId != attendeeDb.PaymentMethodId)
                        {
                            attendeeDb.PaymentMethodId = attendee.PaymentMethodId;
                        }
                        _uow.Attendees.Update(attendeeDb);
                        await _uow.SaveChangesAsync();
                    }
                    else if (attendeeDb.AttendeeType.Value == AttendeeType.Company)
                    {
                        attendeeDb.CompanyName = attendee.CompanyName;
                        attendeeDb.RegistryCode = attendeeDb.RegistryCode;
                        if (attendee.NumberOfPeopleFromCompany != null && attendeeDb.NumberOfPeopleFromCompany != null)
                        {
                            attendeeDb.NumberOfPeopleFromCompany = attendee.NumberOfPeopleFromCompany.Value;
                        }
                        if (attendee.CompanyAdditionalInfo != null)
                        {
                            attendeeDb.CompanyAdditionalInfo = attendee.CompanyAdditionalInfo;

                            if (attendee.PaymentMethodId != attendeeDb.PaymentMethodId)
                            {
                                attendeeDb.PaymentMethodId = attendee.PaymentMethodId;
                            }
                        }
                        
                        if (attendee.CompanyName != null && !attendeeDb.CompanyName.Equals(attendee.CompanyName) && !attendeeDb.RegistryCode.Equals(attendee.RegistryCode) )
                        {
                            attendeeDb.CompanyName = attendee.CompanyName;
                            attendeeDb.RegistryCode = attendee.RegistryCode;
                            var isRegistered = await _uow.Attendees.IsAttendeeAlreadyRegisteredAsync(attendeeDb.AttendeeType.Value, companyName: attendee.CompanyName, registeryCode: attendee.RegistryCode);
                            if (isRegistered.Value == true)
                            {
                                return BadRequest("Sisestatud nime/registrikoodiga ettevõte on juba registeeritud! Palun kontrollige andmeid");
                            }
                        }
                       

                        
                    }
                    _uow.Attendees.Update(attendeeDb);
                    await _uow.SaveChangesAsync();

                    if (attendeeDb.NumberOfPeopleFromCompany != null)
                    {
                        eventAndAttendeDb.NumberOfPeople = attendeeDb.NumberOfPeopleFromCompany.Value;
                    }

                    _uow.EventsAndAttendes.Update(eventAndAttendeDb);
                    await _uow.SaveChangesAsync();

                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Attendees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<ActionResult<AttendeeDTO>> PostAttendee(AttendeeDTO attendee, [FromRoute] int id)
        {
            int? attendeeId = 0;
            var eventDb = await _uow.Events.GetEventByIdAsync(id);
            if (eventDb == null)
            {
                return NotFound("Kirjet ei leitud!");
            }
            if (eventDb.EventDateAndTime < DateTime.Now)
            {
                return NotFound("Kirjet ei leitud!");
            }
            if (attendee.AttendeeType == null)
            {
                return BadRequest("Andmed kirje loomiseks puuduvad!");
            }
            if (attendee.AttendeeType.Value == AttendeeType.Person)
            {
                var isAlreadyRegistered = await _uow.Attendees.IsAttendeeAlreadyRegisteredAsync(attendee.AttendeeType.Value, attendee.PersonalIdentifier, null, null);
                if (isAlreadyRegistered.Value == true)
                {
                    return BadRequest("Eraisik juba lisatud!");
                }
                if (attendee.SurName.IsNullOrEmpty() || attendee.SurName.IsNullOrEmpty() || attendee.GivenName.IsNullOrEmpty() || attendee.PersonalIdentifier.IsNullOrEmpty())
                {
                    return BadRequest("Andmed kirje loomiseks puuduvad!");
                }
                if (attendee.SurName!.Length > 64 || attendee.GivenName!.Length > 64)
                {
                    return BadRequest("Sisestava teksti pikkus peab olema 1 kuni 64 tähemärki!");
                }
                if (attendee.PersonalIdentifier!.Length != 11)
                {
                    return BadRequest("Eesti isikukoodi pikkuseks on 11 numbrit! Palun sisestage uus!");
                }
                if (!attendee.PersonAdditionalInfo.IsNullOrEmpty())
                {
                    if (attendee.PersonAdditionalInfo!.Length > 1000)
                    {
                        return BadRequest("Sisestatud teksti pikkus võib olla kuni 1000 tähemärki!");
                    }
                }


                _uow.Attendees.Add(attendee);
                await _uow.SaveChangesAsync();

                if (attendee.AttendeeType == AttendeeType.Person)
                {
                    attendeeId = _uow.Attendees.GetPersonAttendeeId(attendee.PersonalIdentifier, attendee.SurName, attendee.GivenName);
                    if (attendeeId.HasValue && attendee.AttendeeType == AttendeeType.Person)
                    {
                        var attendeeAndEvent = new EventAndAttendeeDTO()
                        {

                            AttendeeId = attendeeId.Value,
                            EventId = eventDb.Id,
                            NumberOfPeople = 1
                        };
                        _uow.EventsAndAttendes.Add(attendeeAndEvent);
                        await _uow.SaveChangesAsync();
                    }
                }

            }

            else if (attendee.AttendeeType.Value == AttendeeType.Company)
            {
                var isAlreadyRegistered = await _uow.Attendees.IsAttendeeAlreadyRegisteredAsync(AttendeeType.Company, null, attendee.CompanyName, attendee.RegistryCode);
                if (isAlreadyRegistered.Value == true)
                {
                    return BadRequest("Ettevõtte juba lisatud!");
                }
                if (attendee.CompanyName.IsNullOrEmpty() || attendee.RegistryCode.IsNullOrEmpty())
                {
                    return BadRequest("Andmed kirje loomiseks puuduvad!");
                }
                if (attendee.CompanyName!.Length > 64)
                {
                    return BadRequest("Sisestava teksti pikkus peab olema 1 kuni 64 tähemärki!");
                }
                if (attendee.RegistryCode!.Length != 8)
                {
                    return BadRequest("Eesti registrikoodi pikkuseks on 8 numbrit! Palun sisestage uus!");
                }
                if (attendee.NumberOfPeopleFromCompany!.Value <= 0 && attendee.NumberOfPeopleFromCompany.Value > 250)
                {
                    return BadRequest("Ettevõttest tulevate osavõtjate peab jääma vahemikku 1 kuni 250! Palun sisestage uus!");
                }
                if (!attendee.PersonAdditionalInfo.IsNullOrEmpty())
                {
                    if (attendee.PersonAdditionalInfo!.Length > 5000)
                    {
                        return BadRequest("Sisestatud teksti pikkus võib olla kuni 5000 tähemärki!");
                    }
                }


                _uow.Attendees.Add(attendee);
                await _uow.SaveChangesAsync();

                if (attendee.AttendeeType.Value == AttendeeType.Company)
                {
                    attendeeId = _uow.Attendees.GetCompanyAttendeeId(attendee.CompanyName, attendee.RegistryCode);
                    if (attendeeId.HasValue && attendee.AttendeeType == AttendeeType.Company)
                    {
                        var attendeeAndEvent = new EventAndAttendeeDTO()
                        {

                            AttendeeId = attendeeId.Value,
                            EventId = eventDb.Id,
                            NumberOfPeople = attendee.NumberOfPeopleFromCompany.Value
                        };
                        _uow.EventsAndAttendes.Add(attendeeAndEvent);
                        await _uow.SaveChangesAsync();
                    }
                }

            }

            return CreatedAtAction("GetAttendee", new { id = attendee.Id }, attendee);
        }

        // DELETE: Events/1/Attendees/Delete/5
        [HttpDelete("{id}/{eventId}")]

        public async Task<IActionResult> DeleteAttendee(int id, int eventId)
        {
            var attendee = await _uow.Attendees.GetAttendeeByIdAsync(id, true);
            if (attendee == null)
            {
                return NotFound("Olemit ei leitud!");
            }

            var eventDb = await _uow.Events.GetEventByIdAsync(eventId);
            if (eventDb == null)
            {
                return BadRequest("Olemit ei leitud!");
            }
            var isAttendeeAttendingOtherEvents = await _uow.Attendees
                .IsAttendeeAttendingAnyEventsAsync(attendee.Id);

            if (isAttendeeAttendingOtherEvents)
            {
                var eventAndAttendee = await _uow.EventsAndAttendes
                    .GetEventAndAttendeeDTOAsync(eventDb.Id, attendee.Id);
                if (eventAndAttendee == null)
                {
                    return BadRequest("Olemit ei leitud!");
                }
                _uow.EventsAndAttendes.Remove(eventAndAttendee);
                await _uow.SaveChangesAsync();
            }
            else
            {
                _uow.Attendees.Remove(attendee);
                await _uow.SaveChangesAsync();
            }

            return NoContent();
        }
        

        private bool AttendeeExists(int id)
        {
            return _uow.Attendees.Any(e => e!.Id == id);
        }

        [HttpPost("AddAttendeeToAnotherEvent/{id}")]

        public async Task<ActionResult> AddAttendeeToAnotherEventApi(int id, AttendeeDetailDTO attendeeDetails)
        {
            var attendeeDb = await _uow.Attendees.GetAttendeeByIdAsync(id);
            if (attendeeDb == null)
            {
                return NotFound();
            }


            if (attendeeDb.AttendeeType == AttendeeType.Company &&
                attendeeDetails.NumberOfPeople != attendeeDb.NumberOfPeopleFromCompany)
            {
                attendeeDb.NumberOfPeopleFromCompany += attendeeDetails.NumberOfPeople;
            }

            var eventAndAttendee = new EventAndAttendeeDTO()
            {
                AttendeeId = attendeeDb.Id,
                EventId = attendeeDetails.EventId,
                NumberOfPeople = attendeeDb.AttendeeType!.Value == AttendeeType.Company ? attendeeDetails.NumberOfPeople : 1
            };
            _uow.EventsAndAttendes.Add(eventAndAttendee);
            await _uow.SaveChangesAsync();


            return NoContent();
        }

        [HttpGet("getFutureEvents/{id}")]
        public async Task<ActionResult<IEnumerable<EventDTO?>>> GetAllFutureEventsForAnAttendeeOrderByTimeAndName(int id)
        {

            var events = await _uow.Events.GetAllFutureEventsOrderedByTimeAndNameAsync(id);
            return Ok(events);
        }
    }
}
