﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Git_WCF_Heroes_App
{
    public class SuperHero
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HeroName { get; set; }
        public string PlaceOfBirth { get; set; }
        public int Combat { get; set; }
    }
}