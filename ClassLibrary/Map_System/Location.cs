using ClassLibrary.Activity_System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Map_System
{
    public class Location
    {
        #region Properties
        //----------------------------------- Properties -----------------------------------
        public string Name { get; private set; }
        public string Description { get; private set; }
        public (int X, int Y) Coordinates { get; private set; }
        public List<Location> ConnectedLocations { get; set; } = new List<Location>();
        public List<IActivity> Activities { get; } = new();

        #endregion


        #region Functions
        //----------------------------------- Functions -----------------------------------

        public void ConnectTo(Location other)
        {
            if (!ConnectedLocations.Contains(other))
                ConnectedLocations.Add(other);

            if (!other.ConnectedLocations.Contains(this))
                other.ConnectedLocations.Add(this);
        }

        public double GetTravelTime(Location destination, double speed = 1.0)
        {
            int dx = Coordinates.X - destination.Coordinates.X;
            int dy = Coordinates.Y - destination.Coordinates.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return distance / speed;
        }

        public void AddActivity(IActivity activity)
        {
            if (!Activities.Contains(activity))
            {
                Activities.Add(activity);
            }
        }

        public void RemoveActivity(IActivity activity)
        {
            Activities.Remove(activity);
        }

        public string GetActivitiesList()
        {
            if (Activities.Count == 0)
                return "No activities available here.";

            return string.Join(", ", Activities.Select(a => a.Name));
        }

        public bool HasActivity<T>() where T : IActivity
        {
            return Activities.Any(a => a is T);
        }

        public IActivity? GetActivity<T>() where T : IActivity
        {
            return Activities.FirstOrDefault(a => a is T);
        }

        #endregion

        #region Constructors
        //----------------------------------- Constructors -----------------------------------
        public Location(string name, string description, int x, int y)
        {
            Name = name;
            Description = description;
            Coordinates = (x, y);
        }

        #endregion
    }
}
