using System;
using System.Collections.Generic;
using System.Linq;
using static TicketsConsole.Program;

/*

Let's say we're running a small entertainment business as a start-up. This means we're selling tickets to live events on a website. An email campaign service is what we are going to make here. We're building a marketing engine that will send notifications (emails, text messages) directly to the client and we'll add more features as we go.

Please, instead of debuging with breakpoints, debug with "Console.Writeline();" for each task because the Interview will be in Coderpad and in that platform you cant do Breakpoints.

*/

namespace TicketsConsole
{
    public class Program
    {

        public static readonly IDictionary<string, int> CityDistance = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            /*

            1. You can see here a list of events, a customer object. Try to understand the code, make it compile. 

           2. The goal is to create a MarketingEngine class sending all events through the constructor as parameter and make it print the events that are happening in the same city as the customer. To do that, inside this class, create a SendCustomerNotifications method which will receive a customer as parameter and an Event parameter and will mock the the Notification Service API. DON’T MODIFY THIS METHOD, unless you want to add the price to the console.writeline for task 7. Add this ConsoleWriteLine inside the Method to mock the service. Inside this method you can add the code you need to run this task correctly but you cant modify the console writeline: Console.WriteLine($"{customer.Name} from {customer.City} event {e.Name} at {e.Date}");


            3. As part of a new campaign, we need to be able to let customers know about events that are coming up close to their next birthday. You can make a guess and add it to the MarketingEngine class if you want to. So we still want to keep how things work now, which is that we email customers about events in their city or the event closest to next customer's birthday, and then we email them again at some point during the year. The current customer, his birthday is on may. So it's already in the past. So we want to find the next one, which is 23. How would you like the code to be built? We don't just want functionality; we want more than that. We want to know how you plan to make that work. Please code it.

            4. The next requirement is to extend the solution to be able to send notifications for the five closest events to the customer. The interviewer here can paste a method to help you, or ask you to search it. We will attach a way to calculate the distance.

public record City(string Name, int X, int Y);
|public static readonly IDictionary<string, City> Cities = new Dictionary<string, City>()
        {
            { "New York", new City("New York", 3572, 1455) },
            { "Los Angeles", new City("Los Angeles", 462, 975) },
            { "San Francisco", new City("San Francisco", 183, 1233) },
            { "Boston", new City("Boston", 3778, 1566) },
            { "Chicago", new City("Chicago", 2608, 1525) },
            { "Washington", new City("Washington", 3358, 1320) },
        };
var customerCityInfo = Cities.Where(c => c.Key == city).Single().Value;
var distance = Math.Abs(customerCityInfo.X - eventCityInfo.X) + Math.Abs(customerCityInfo.Y - eventCityInfo.Y);

            5. If the calculation of the distances is an API call which could fail or is too expensive, how will you improve the code written in 4? Think in caching the data which could be code it as a dictionary. You need to store the distances between two cities. Example:

            New York - Boston => 400 
            Boston - Washington => 540
            Boston - New York => Should not exist because "New York - Boston" is already stored and the distance is the same. 

            6. If the calculation of the distances is an API call which could fail, what can be done to avoid the failure? Think in HTTPResponse Answers: Timeoute, 404, 403. How can you handle that exceptions? Code it.

            7.  If we also want to sort the resulting events by other fields like price, etc. to determine whichones to send to the customer, how would you implement it? Code it.
            */

            var events = new List<Event>{
                new Event(1, "Phantom of the Opera", "New York", new DateTime(2023,12,23), 10),
                new Event(2, "Metallica", "Los Angeles", new DateTime(2023,12,02), 11),
                new Event(3, "Metallica", "New York", new DateTime(2023,12,06), 12),
                new Event(4, "Metallica", "Boston", new DateTime(2023,10,23), 13),
                new Event(5, "LadyGaGa", "New York", new DateTime(2023,09,20), 14),
                new Event(6, "LadyGaGa", "Boston", new DateTime(2023,08,01), 15),
                new Event(7, "LadyGaGa", "Chicago", new DateTime(2023,07,04), 16),
                new Event(8, "LadyGaGa", "San Francisco", new DateTime(2023,07,07), 17),
                new Event(9, "LadyGaGa", "Washington", new DateTime(2023,05,22), 18),
                new Event(10, "Metallica", "Chicago", new DateTime(2023,01,01), 19),
                new Event(11, "Phantom of the Opera", "San Francisco", new DateTime(2023,07,04), 20),
                new Event(12, "Phantom of the Opera", "Chicago", new DateTime(2024,05,15), 21)
            };

            var customer = new Customer()
            {
                Id = 1,
                Name = "John",
                City = "New York",
                BirthDate = new DateTime(1995, 05, 10)
            };
            
            //2.
            var marketingEngine = new MarketingEngine(events, customer);
            marketingEngine.SendSameCityCampaign();
            
            //3.
            marketingEngine.SendBirthdayCampaign();
            
            //4.
            marketingEngine.SendClosestEventCampaign();

        }
        
        public record City(string Name, int X, int Y);
        public static readonly IDictionary<string, City> Cities = new Dictionary<string, City>()
        {
            { "New York", new City("New York", 3572, 1455) },
            { "Los Angeles", new City("Los Angeles", 462, 975) },
            { "San Francisco", new City("San Francisco", 183, 1233) },
            { "Boston", new City("Boston", 3778, 1566) },
            { "Chicago", new City("Chicago", 2608, 1525) },
            { "Washington", new City("Washington", 3358, 1320) },
        };
        
        //7.
        public class Event
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public DateTime Date { get; set; }
            public Decimal Price { get; set; }

            public Event(int id, string name, string city, DateTime date, Decimal price)
            {
                this.Id = id;
                this.Name = name;
                this.City = city;
                this.Date = date;
                this.Price = price;
            }
        }

        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public DateTime BirthDate { get; set; }
        }

       
        /*-------------------------------------
        Coordinates are roughly to scale with miles in the USA
           2000 +----------------------+  
                |                      |  
                |                      |  
             Y  |                      |  
                |                      |  
                |                      |  
                |                      |  
                |                      |  
             0  +----------------------+  
                0          X          4000
        ---------------------------------------*/

    }
    
    public class MarketingEngine
    {
        private readonly IEnumerable<Event> _events;
        private readonly Customer _customer;

        public MarketingEngine(IEnumerable<Event> events, Customer customer)
        {
            _events = events;
            _customer = customer;
        }
        public void SendSameCityCampaign()
        {
            foreach (var e in _events.Where(c=>c.Price>10))
            {
                if(string.Equals(e.City, _customer.City, StringComparison.CurrentCultureIgnoreCase))
                    SendCustomerNotifications(_customer,e);
            }
        }
        public void SendBirthdayCampaign()
        {
            DateTime today = DateTime.Today;
            
            //check if birthday is past within year
            DateTime nextBirthday = _customer.BirthDate.Day > today.Day
                ? new DateTime(today.Year, _customer.BirthDate.Month, _customer.BirthDate.Day)
                : new DateTime(today.Year + 1, _customer.BirthDate.Month, _customer.BirthDate.Day);

            var closestEvent = _events.Where(e => e.Date < nextBirthday).MaxBy(e => e.Date);

            if (closestEvent != null)
            {
                SendCustomerNotifications(_customer,closestEvent);
            }
        }
        
        public void SendClosestEventCampaign()
        {
            var closestFive = _events.OrderBy(e => GetDistance(_customer.City,e.City)).Take(5);

            if (!closestFive.Any()) return;
            foreach (var @event in closestFive)
            {
                SendCustomerNotifications(_customer, @event);
            }
        }
        //5,6.
        private int GetDistance(String from, String to)
        {
            if (string.Equals(from, to, StringComparison.CurrentCultureIgnoreCase)) 
                return 0;
            
            CityDistance.TryGetValue($"{from}-{to}", out var distance);

            if (distance > 0) return distance;
            CityDistance.TryGetValue($"{to}-{from}", out distance);
            
            if(distance > 0) return distance;

            try
            {
                distance = Math.Abs(Cities[from].X - Cities[to].X) + Math.Abs(Cities[from].Y - Cities[to].Y);
                CityDistance.Add($"{from}-{to}",distance);
            }
            catch
            {
                //log if required
                //you might want to return Int.Max here to avoid the order considering these records
            }

            return distance;
        }
        
        private void SendCustomerNotifications(Customer customer, Event customerEvent)
        {
            Console.WriteLine($"{customer.Name} from {customer.City} event {customerEvent.Name} at {customerEvent.Date} ");
        }
    }

}
