using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Models
{
    public class Crash
    {
        [Key]
        [Required]
        public int crash_id { get; set; }

        [Required]
        public string crash_date { get; set; }
        [Required]
        public string crash_time { get; set; }
        [Required]
        public int route { get; set; }
        [Required]
        public double milepoint { get; set; }
        [Required]
        public double lat_utm_y { get; set; }
        [Required]
        public double long_utm_x { get; set; }
        [Required]
        public string main_road_name { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string county_name { get; set; }
        [Required]
        public int crash_severity_id { get; set; }



        //All of the Binary options
        [Required]
        public string work_zone_related  { get; set; }
        [Required]
        public string pedestrian_involved { get; set; }
        [Required]
        public string bicyclist_involved { get; set; }
        [Required]
        public string motorcycle_involved { get; set; }
        [Required]
        public string improper_restraint { get; set; }
        [Required]
        public string unrestrained { get; set; }
        [Required]
        public string dui { get; set; }
        [Required]
        public string intersection_related { get; set; }
        [Required]
        public string wild_animal_related { get; set; }
        [Required]
        public string domestic_animal_related { get; set; }
        [Required]
        public string overturn_rollover { get; set; }
        [Required]
        public string commercial_motor_veh_involved { get; set; }
        [Required]
        public string teenage_driver_involved { get; set; }
        [Required]
        public string older_driver_involved { get; set; }
        [Required]
        public string night_dark_condition { get; set; }
        [Required]
        public string single_vehicle { get; set; }
        [Required]
        public string distracted_driving { get; set; }
        [Required]
        public string drowsy_driving { get; set; }
        [Required]
        public string roadway_departure { get; set; }
    }
}
