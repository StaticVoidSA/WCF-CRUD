using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Git_WCF_Heroes_App
{
    public class Data
    {
        public static List<SuperHero> SuperHeroes = new List<SuperHero>
        {
            new SuperHero { Id = 0, FirstName = "Peter", LastName = "Parker", HeroName = "SpiderMan", PlaceOfBirth = "New York", Combat = 85 },
            new SuperHero { Id = 1, FirstName = "Bruce", LastName = "Wayne", HeroName = "Batman", PlaceOfBirth = "Gotham City", Combat = 100 },
            new SuperHero { Id = 2, FirstName = "Tony", LastName = "Stark", HeroName = "Iron Man", PlaceOfBirth = "Long Island", Combat = 65 },
            new SuperHero { Id = 3, FirstName = "Bruce", LastName = "Banner", HeroName = "Hulk", PlaceOfBirth = "Dayton", Combat = 85 },
            new SuperHero { Id = 4, FirstName = "James", LastName = "Howlett", HeroName = "Wolverine", PlaceOfBirth = "Alberta", Combat = 100 },
            new SuperHero { Id = 5, FirstName = "Stephen", LastName = "Strange", HeroName = "Doctor Strange", PlaceOfBirth = "Philidelphia", Combat = 60 },
            new SuperHero { Id = 6, FirstName = "Thor", LastName = "Odinson", HeroName = "Thor", PlaceOfBirth = "Asgard", Combat = 85 }
        };
    }
}