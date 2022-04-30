using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if(!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return "Race cannot be completed because both racers are not available!";
            }
            else if(racerOne.IsAvailable() && racerTwo.IsAvailable())
            {
                double racer1Stats = racerOne.Car.HorsePower * racerOne.DrivingExperience;
                if(racerOne.RacingBehavior == "strict")
                {
                    racer1Stats *= 1.2;
                }
                else
                {
                    racer1Stats *= 1.1;
                }
                double racer2Stats = racerTwo.Car.HorsePower * racerTwo.DrivingExperience;
                if (racerTwo.RacingBehavior == "strict")
                {
                    racer2Stats *= 1.2;
                }
                else
                {
                    racer2Stats *= 1.1;
                }
                string winner;
                if(racer1Stats > racer2Stats)
                {
                    winner = racerOne.Username;
                }
                else
                {
                    winner = racerTwo.Username;
                }
                racerOne.Race();
                racerTwo.Race();
                return $"{racerOne.Username} has just raced against {racerTwo.Username}! {winner} is the winner!";
            }
            else
            {
                if (racerOne.IsAvailable())
                {
                    return $"{racerOne.Username} wins the race! {racerTwo.Username} was not available to race!";
                }
                else
                {
                    return $"{racerTwo.Username} wins the race! {racerOne.Username} was not available to race!";
                }
            }
        }
    }
}
