using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LeaseWebAssignment.Models;
using System.Xml;


namespace LeaseWebAssignment.DAL
{
    public class CompanyInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CompanyContext>
    {
        protected override void Seed(CompanyContext context)
        {
            // Add countries, this will be done by parsing an XML file
            XmlDocument doc = new XmlDocument();
            doc.Load("Default_Data/countries.xml");
            var countries = new List<Country>();
            foreach (XmlNode node in doc.SelectNodes("//country"))
            {
                try {
                    countries.Add(new Country { code = node.Attributes["code"].InnerText, iso = Int16.Parse(node.Attributes["iso"].InnerText), name = node.InnerText });
                }
                catch (Exception anyE)
                {
                    Console.WriteLine("Exception while parsing countries : " + anyE.Message);
                }
            }

            countries.ForEach(s => context.Countries.Add(s));
            context.SaveChanges();

            // Add cities
            var cities = new List<City>();
            foreach (Country country in countries)
            {
                cities.Add(new City { country = country, name = "other" });
            }

            cities.ForEach(s => context.Cities .Add(s));
            context.SaveChanges();
        }
    }
}