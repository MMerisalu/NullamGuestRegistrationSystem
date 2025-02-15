﻿using App.DAL.DTO;
using App.Enum;
using Base.Contracts.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Contracts.DAL.IAppRepositories
{
    public interface IAttendeeRepository : IEntityRepository<AttendeeDTO, int>,
    IAttendeeRepositoryCustom<AttendeeDTO>
    {

    }

    public interface IAttendeeRepositoryCustom<TEntity>
    {
        Task<List<TEntity?>>? GetAllAttendeesOfEventOrderedByNameAsync(int eventId, bool noTracking = true, bool noIncludes = false);
        List<TEntity?>? GetAllAttendeesOfEventOrderedByName(int eventId, bool noTracking = true, bool noIncludes = false);
        Task<IEnumerable<TEntity?>> GetAllAttendeesOrderedByNameAsync(bool noTracking = true, bool noIncludes = false);
        IEnumerable<TEntity?> GetAllAttendeesOrderedByName( bool noTracking = true, bool noIncludes = false);
        int? GetPersonAttendeeId(string personalIdentifier, string surName, string givenName, bool noTracking = true, bool noIncludes = false);
        int? GetCompanyAttendeeId(string companyName, string registryCode, bool noTracking = true, bool noIncludes = false);
        Task<TEntity?> GetAttendeeByIdAsync(int id, bool noTracking = true, bool noIncludes = false);
        TEntity? GetAttendeeById(int id, bool noTracking = true, bool noIncludes = false);
        Task<bool?> IsAttendeeAlreadyRegisteredAsync(AttendeeType attendeeType, string? personalIdentifier = null, string? companyName = null, string? registeryCode = null, bool noTracking = true, bool noIncludes = false);
        bool? IsAttendeeAlreadyRegistered(AttendeeType attendeeType, string? personalIdentifier = null, string? companyName = null, string? registeryCode = null, bool noTracking = true, bool noIncludes = false);
        Task<bool> IsAttendeeAttendingAnyEventsAsync(int attendeeId, bool noTracking = true, bool noIncludes = false);
        bool IsAttendeeAttendingAnyEvents(int attendeeId, bool noTracking = true, bool noIncludes = false);
        IEnumerable<int>? GetAllEventsForAnAttendee(int attendeeId, bool noTracking = true, bool noIncludes = false);
        Task<bool> IsConnectedToAnyPaymentMethodsAsync(int paymentMethodId, bool noTracking = true, bool noIncludes = false);
        bool IsConnectedToAnyPaymentMethods(int paymentMethodId, bool noTracking = true, bool noIncludes = false);
        Task<int> NumberOfEventsForAttendeeAsync(int attendeeId, bool noTracking = true, bool noIncludes = false);
       int NumberOfEventsForAttendee(int attendeeId, bool noTracking = true, bool noIncludes = false);

    }
}
