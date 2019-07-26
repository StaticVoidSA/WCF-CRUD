using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace Git_WCF_Heroes_App.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SuperHeroService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SuperHeroService.svc or SuperHeroService.svc.cs at the Solution Explorer and start debugging.
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SuperHeroService
    {
        // Get Request - GetAllHeroes
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        public List<SuperHero> GetAllHeroes()
        {
            var result = Data.SuperHeroes;
            return result;
        }

        // Get Request - Custom URL Request - /Service/SuperHeroService.svc/GetHero/{id}
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHero/{id}")]
        public SuperHero GetHero(string id)
        {
            var result = Data.SuperHeroes.Find(sh => sh.Id == int.Parse(id));
            return result;
        }

        // Post Request - AddHero
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddHero", Method = "POST")]
        public SuperHero AddHero(SuperHero hero)
        {
            hero.Id = Data.SuperHeroes.Max(sh => sh.Id) + 1;
            Data.SuperHeroes.Add(hero);
            return hero;
        }

        // Put Request - UpdateHero
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateHero/{id}", Method = "PUT")]
        public SuperHero UpdateHero(SuperHero updatedHero, string id)
        {
            SuperHero hero = Data.SuperHeroes.Where(sh => sh.Id == int.Parse(id)).FirstOrDefault();

            hero.FirstName = updatedHero.FirstName;
            hero.LastName = updatedHero.LastName;
            hero.HeroName = updatedHero.HeroName;
            hero.PlaceOfBirth = updatedHero.PlaceOfBirth;
            hero.Combat = updatedHero.Combat;

            return updatedHero;
        }

        // Delete Request - DeleteHero
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteHero/{id}", Method = "DELETE")]
        public List<SuperHero> DeleteHero(string id)
        {
            Data.SuperHeroes = Data.SuperHeroes.Where(sh => sh.Id != int.Parse(id)).ToList();

            return Data.SuperHeroes;
        }

        // Get Request - SearchHero using Linq
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SearchHero/{searchText}", Method = "GET")]
        public List<SuperHero> SearchHero(string searchText)
        {
            List<SuperHero> result = Data.SuperHeroes
                .Where<SuperHero>(sh => sh.FirstName.ToLower().Contains(searchText)
                  || sh.FirstName.Contains(searchText)
                  || sh.LastName.Contains(searchText)
                  || sh.HeroName.Contains(searchText)
                  || sh.PlaceOfBirth.Contains(searchText)
                  || sh.LastName.ToLower().Contains(searchText)
                  || sh.HeroName.ToLower().Contains(searchText)
                  || sh.Combat.ToString().Contains(searchText)
                  || sh.PlaceOfBirth.ToLower().Contains(searchText)).ToList<SuperHero>();

            // Return 404 error if no record found
            if (result.Count == 0)
            {
                // Catch a 404 Error
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;

                // Custom Status Code Error Return
                WebOperationContext.Current.OutgoingResponse.StatusDescription = "Oh snap! There is no such record.";

                // Create an exception
                throw new WebFaultException<string>("No hero found", System.Net.HttpStatusCode.NotFound);
            }

            return result;
        }

        // Get Request - Order a list using Linq
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSortedHeroList/{type}", Method = "GET")]
        public List<SuperHero> GetSortedHeroList(string type)
        {
            switch (type)
            {
                case "firstname":
                    return Data.SuperHeroes.OrderBy(hero => hero.FirstName).ThenBy(hero => hero.LastName).ToList();
                case "lastname":
                    return Data.SuperHeroes.OrderBy(hero => hero.LastName).ThenBy(hero => hero.FirstName).ToList();
                case "hero":
                    return Data.SuperHeroes.OrderBy(hero => hero.HeroName).ThenBy(hero => hero.FirstName).ToList();
                case "birthplace":
                    return Data.SuperHeroes.OrderBy(hero => hero.PlaceOfBirth).ThenBy(hero => hero.FirstName).ToList();
                case "combat":
                default:
                    return Data.SuperHeroes.OrderBy(hero => hero.Combat).ThenBy(hero => hero.FirstName).ToList();
            }
        }

        // Get Request - Fight backend method
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Fight/{id1}/{id2}", Method = "GET")]
        public string Fight(string id1, string id2)
        {
            SuperHero hero1 = Data.SuperHeroes.Find(hero => hero.Id == int.Parse(id1));
            SuperHero hero2 = Data.SuperHeroes.Find(hero => hero.Id == int.Parse(id2));

            if (hero1.Combat > hero2.Combat)
            {
                return $"{hero1.HeroName} wins!";
            }
            if (hero2.Combat > hero1.Combat)
            {
                return $"{hero2.HeroName} wins!";
            }

            return "Its a tie!";
        }
    }
}
