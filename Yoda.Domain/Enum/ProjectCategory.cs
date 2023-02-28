using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoda.Domain.Enum
{
    public enum ProjectCategory
    {
        [Display (Name ="Shop")]    Shop = 0,
        [Display(Name = "BaberShop")]   BaberShop = 1,
        [Display(Name = "Parking")]    Parking = 2,
        [Display(Name = "Hairdressing Services")] HairdressingServices = 3,
        [Display(Name = "Manicure")] Manicure = 4,
        [Display(Name = "Eyelashes")] Eyelashes = 5,
        [Display(Name = "Depilation, epilation")] DepilationEpilation = 6,
        [Display(Name = "Brows")] Brows = 7,
        [Display(Name = "Cosmetology, care")] CosmetologyСare = 8,
        [Display(Name = "Visage")] Visage = 9,
        [Display(Name = "Tattoo")] Tattoo = 10,
        [Display(Name = "Piercing")] Piercing = 11,
        [Display(Name = "Mustache, beard")] MustacheBeard = 12,
        [Display(Name = "Solarium")] Solarium = 13,
        [Display(Name = "Veterinary")] Veterinary = 14,
        [Display(Name = "Grooming")] Grooming = 15,
        [Display(Name = "Medicine")] Medicine = 16,
        [Display(Name = "Massage")] Massage = 17,
        [Display(Name = "Dentistry")] Dentistry = 18,
        [Display(Name = "Analyzes")] Analyzes = 19,
        [Display(Name = "Fitness")] Fitness = 20,
        [Display(Name = "Martial arts")] MartialArts = 21,
        [Display(Name = "Yoga")] Yoga = 22,
        [Display(Name = "Dancing")] Dancing = 23,
        [Display(Name = "Quest")] Quest = 24,
        [Display(Name = "SPA")] SPA = 25,
        [Display(Name = "Baths, saunas")] BathsSaunas = 26,
        [Display(Name = "Restaurant")] Restaurant = 27,
        [Display(Name = "Bowling")] Bowling = 28,
        [Display(Name = "Driving school")] DrivingSchool = 29,
        [Display(Name = "Tutor")] Tutor = 30,
        [Display(Name = "Courses")] Courses = 31,
        [Display(Name = "Music")] Music = 32,



    }
}
