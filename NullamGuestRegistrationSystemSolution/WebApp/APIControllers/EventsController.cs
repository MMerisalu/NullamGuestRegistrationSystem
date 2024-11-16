﻿using System;
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
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApp.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public EventsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDTO?>>> GetEvents()
        {
            return Ok(await _uow.Events.GetAllEventsDTOOrderedByNameAsync());
        }

        

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, EventDTO eventDTO)
        {
            if (id != eventDTO.Id)
            {
                return NotFound();
            }

            var eventDb = _uow.Events.GetEventById(id, noIncludes: true);
            
            if (eventDb == null)
                return NotFound();
            try
            {
                eventDb.Name = eventDTO.Name;
                eventDb.EventDateAndTime = eventDTO.EventDateAndTime;
                eventDb.Location = eventDTO.Location;
                eventDb.AdditionalInfo = eventDTO.AdditionalInfo;
                _uow.Events.Update(eventDb);
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_uow.Events.Exists(id))
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

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventDTO>> PostEvent(EventDTO eventDTO)
        {
            if (eventDTO == null) 
            {
                return BadRequest();
            }

            _uow.Events.Add(eventDTO);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = eventDTO.Id }, eventDTO);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventDTO = await _uow.Events.GetEventByIdAsync(id, noIncludes: true);
            if (eventDTO == null)
            {
                return NotFound();
            }

            _uow.Events.Remove(eventDTO);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(int id)
        {
            return _uow.Events.Exists(id);
        }
    }
}