﻿using App.Domain;
using Base.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UoN.ExpressiveAnnotations.Net8.Attributes;

namespace WebApp.ViewModels
{
    public class PersonDetails { }
    public class CompanyDetails { }

    public class CreateEditAttendeeVM: DomainEntityId<int>
    {

        //public int AttendeeId { get; set; }

        [DisplayName("Osavõtja tüüp")]
        [Required(ErrorMessage = "Osavõtja tüübi valimine on kohustuslik!")]
        [EnumDataType(typeof(AttendeeType))]
        public AttendeeType? AttendeeType { get; set; }

        //[Required(ErrorMessage = "Väli eesnimi on kohustuslik!")]
        [RequiredIf("AttendeeType == App.Domain.AttendeeType.Person")]
        [MaxLength(64, ErrorMessage = "Väljale eesnimi sisestava teksti pikkus on maksimaalselt 64 tähemärki!")]
        [StringLength(64, MinimumLength = 1)]
        [DisplayName("Eesnimi")]
        public string? SurName { get; set; }

        //[Required(ErrorMessage = "Väli perekonnanimi on kohustuslik!")]
        [RequiredIf("AttendeeType == App.Domain.AttendeeType.Person")]
        [MaxLength(64, ErrorMessage = "Väljale perekonnanimi sisestava teksti pikkus on maksimaalselt 64 tähemärki!")]
        [StringLength(64, MinimumLength = 1)]
        [DisplayName("Perekonnanimi")]
        public string? GivenName { get; set; }

        //[Required(ErrorMessage = "Väli isikukood on kohustuslik!")]
        [RequiredIf("AttendeeType == App.Domain.AttendeeType.Person")]
        [RegularExpression("^[0-9]{11,11}$", ErrorMessage = "Eesti isikukood pikkuseks on 11 numbrit! " +
            "Palun sisestage uus isikukood!")]
        [StringLength(11)]
        [DisplayName("Isikukood")]
        public string? PersonalIdentifier { get; set; }

        [MaxLength(1000)]
        [DisplayName("Lisainfo")]

        public string? PersonAdditionalInfo { get; set; }


        //[Required(ErrorMessage = "Väli ettevõtte juurdiline nimi on kohustuslik")]

        [RequiredIf("AttendeeType == App.Domain.AttendeeType.Company")]
        [MaxLength(64, ErrorMessage = "Väljale ettevõtte juurdiline nimi sisestava teksti pikkus on maksimaalselt 64 tähemärki!")]
        [StringLength(64, MinimumLength = 1)]
        [DisplayName("Ettevõtte juurdiline nimi")]
        public string? CompanyName { get; set; }

        //[Required(ErrorMessage = "Väli ettevõtte registrikood on kohustuslik")]
        [RequiredIf("AttendeeType == App.Domain.AttendeeType.Company")]
        [RegularExpression("^[0-9]{8,8}$", ErrorMessage = "Ettevõtte registrikoodi pikkuseks on 8 numbrit! " +
            "Palun sisestage uus registrikood!")]
        [StringLength(8)]
        [DisplayName("Ettevõtte registrikood")]
        public string? RegistryCode { get; set; }

        [RequiredIf("AttendeeType == App.Domain.AttendeeType.Company")]
        //[Required(ErrorMessage = "Väli ettevõttest tulevate osavõtjate arv on kohustuslik")]
        [DisplayName("Ettevõttest tulevate osavõtjate arv")]


        [RequiredIf("AttendeeType == App.Domain.AttendeeType.Company")]
        [RegularExpression("^(?:[1-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|250)$", ErrorMessage = "Ettevõttest tulevate isikute arv võib olla 1 kuni 250. Palun sisestage ettevõttest tulevate isikute arv uuesti.")]
        public int? NumberOfPeopleFromCompany { get; set; }

        [MaxLength(5000)]
        [DisplayName("Lisainfo")]
        public string? CompanyAdditionalInfo { get; set; }
        [DisplayName("Maksemeetod")]


        [Required(ErrorMessage = "Maksemeetodi valimine on kohustuslik!")]
        public int PaymentMethodId { get; set; }

        public SelectList? PaymentMethods { get; set; }
    }
}

