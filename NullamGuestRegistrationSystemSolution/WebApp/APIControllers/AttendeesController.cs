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
        
        public async Task<IActionResult> PutAttendee(int id, AttendeeDTO attendee, [FromRoute]int eventId)
        {
            if (id != attendee.Id)
            {
                return BadRequest();
            }
            var attendeeDb = await _uow.Attendees.GetAttendeeByIdAsync(id, noIncludes: true);
            if (attendeeDb == null)
            {
                return BadRequest("Kirjet ei leitud!");
            }
            var eventDb = await _uow.Events.GetEventByIdAsync(id, noIncludes: true);
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
                        if (attendee.PersonalIdentifier != null)
                        {
                            if (attendee.PersonalIdentifier.Length != 11)
                            {
                                return BadRequest("Eesti isikukoodi pikkuseks on 11 numbrit! Palun sisestage uus!");
                            }
                            var isRegistered = await _uow.Attendees.IsAttendeeAlreadyRegisteredAsync(attendeeDb.AttendeeType.Value, attendee.PersonalIdentifier, null, null, true, true);
                            if (isRegistered.Value == true)
                            {
                                var errorDetails = new { Message = "Sisestatud isikukoodiga eraisik on juba registeeritud!" };
                                return new ObjectResult(errorDetails) { StatusCode = 406 };
                            }

                        }

                        attendeeDb.PersonalIdentifier = attendee.PersonalIdentifier;
                        if (attendee.PersonAdditionalInfo != null)
                        {
                            if (attendee.PersonAdditionalInfo.Length > 1000)
                            {
                                return BadRequest("Sisestatud teksti pikkus võib olla kuni 1000 tähemärki!");
                            }
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
                        if (attendee.RegistryCode.IsNullOrEmpty() || attendee.RegistryCode!.Length != 8)
                        {
                            return BadRequest("Eesti ettevõte registrikoodi pikkuseks on 8 numbrit! Palun sisestage uus!");
                        }
                        if (attendee.CompanyName == attendeeDb.CompanyName)
                        {
                            return BadRequest("Eesti ettevõte registrikoodi muutmisel, tuleb muuta ka ettevõte juuridilist nime! Palun sisestage uus!");
                        }
                        if (attendee.AttendeeType.HasValue)
                        {
                            var isRegistered = await _uow.Attendees.IsAttendeeAlreadyRegisteredAsync(attendee.AttendeeType.Value, null, attendeeDb.CompanyName, attendeeDb.RegistryCode);
                            if (isRegistered.HasValue && isRegistered.Value == true)
                            {
                                var errorDetails = new { Message = "Sisestatud ettevõtte juurdilise nime / registrikoodiga ettevõtte on juba registreeritud!  eraisik on juba registeeritud!" };
                                return new ObjectResult(errorDetails) { StatusCode = 406 };

                            }
                        }

                        attendeeDb.CompanyName = attendee.CompanyName;
                        attendeeDb.RegistryCode = attendeeDb.RegistryCode;
                        if (attendee.NumberOfPeopleFromCompany != null && attendeeDb.NumberOfPeopleFromCompany != null)
                        {
                            if (attendee.NumberOfPeopleFromCompany.Value != attendeeDb.NumberOfPeopleFromCompany.Value)
                            {
                                if (attendee.NumberOfPeopleFromCompany.Value <= 0 || attendee.NumberOfPeopleFromCompany.Value > 250)
                                {
                                    return BadRequest("Ettevõttest tulevate osavõtjate peab jääma vahemikku 1 kuni 250! Palun sisestage uus!");
                                }
                            }
                            attendeeDb.NumberOfPeopleFromCompany = attendee.NumberOfPeopleFromCompany.Value;
                        }
                        if (attendee.CompanyAdditionalInfo != null)
                        {
                            if (attendee.CompanyAdditionalInfo.Length > 5000)
                            {
                                return BadRequest("Sisestatud teksti pikkus võib olla kuni 5000 tähemärki!");
                            }
                            attendeeDb.CompanyAdditionalInfo = attendee.CompanyAdditionalInfo;

                            if (attendee.PaymentMethodId != attendeeDb.PaymentMethodId)
                            {
                                attendeeDb.PaymentMethodId = attendee.PaymentMethodId;
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
            var eventDb = await _uow.Events.GetEventByIdAsync(id);
            if (eventDb == null)
            { return NotFound("Kirjet ei leitud!");
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

                var attendeeAndEvent = new EventAndAttendeeDTO()
                {
                    AttendeeId = attendee.Id,
                    EventId = eventDb.Id,
                    NumberOfPeople = 1
                };
                _uow.EventsAndAttendes.Add(attendeeAndEvent);
                await _uow.SaveChangesAsync();
            }

            else if (attendee.AttendeeType == AttendeeType.Company)
            {
                if (!attendee.CompanyName.IsNullOrEmpty() || attendee.RegistryCode.IsNullOrEmpty())
                {
                    return BadRequest("Andmed kirje loomiseks puuduvad!");
                }

                if (attendee.RegistryCode!.Length != 8)
                {
                    return BadRequest("Eesti ettevõte registrikoodi pikkuseks on 8 numbrit! Palun sisestage uus!");
                }

                if (attendee.NumberOfPeopleFromCompany == null || (attendee.NumberOfPeopleFromCompany.Value <= 0 && attendee.NumberOfPeopleFromCompany.Value > 250))
                {
                    return BadRequest("Ettevõttest tulevate osavõtjate peab jääma vahemikku 1 kuni 250! Palun sisestage uus!");
                }

                if (attendee.CompanyAdditionalInfo!.Length > 5000)
                {
                    return BadRequest("Sisestatud teksti pikkus võib olla kuni 5000 tähemärki!");
                }

                _uow.Attendees.Add(attendee);
                await _uow.SaveChangesAsync();
            }
            var eventAndAttendee = new EventAndAttendeeDTO
            {
                EventId = id,
                AttendeeId = attendee.Id,
                NumberOfPeople = attendee.NumberOfPeopleFromCompany!.Value
            };
            _uow.EventsAndAttendes.Add(eventAndAttendee);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetAttendee", new { id = attendee.Id }, attendee);
        }


        // DELETE: Events/1/Attendees/Delete/5
        [HttpDelete("{id}/{eventId}")]
        
        public async Task<IActionResult> DeleteAttendee(int id, int eventId )
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
