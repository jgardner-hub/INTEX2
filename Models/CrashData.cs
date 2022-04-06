using System;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace INTEX2.Models
{
    public class CrashData
    {
        public float milepoint { get; set; }
        public float lat_utm_y { get; set; }
        public float long_utm_x { get; set; }
        public float work_zone_related { get; set; }
        public float pedestrian_involved { get; set; }
        public float bicyclist_involved { get; set; }
        public float motorcycle_involved { get; set; }
        public float unrestrained { get; set; }
        public float intersection_related { get; set; }
        public float overturn_rollover { get; set; }
        public float teenage_driver_involved { get; set; }
        public float older_driver_involved { get; set; }
        public float night_dark_condition { get; set; }
        public float single_vehicle { get; set; }
        public float distracted_driving { get; set; }
        public float roadway_departure { get; set; }
        public float month { get; set; }
        public float hour { get; set; }
        //public float main_road_name_Other { get; set; }
        //public float crash_severity_id { get; set; }
        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
                    milepoint, lat_utm_y, long_utm_x, work_zone_related,
                    pedestrian_involved, bicyclist_involved, motorcycle_involved, unrestrained,
                    intersection_related, overturn_rollover, teenage_driver_involved, older_driver_involved,
                    night_dark_condition, single_vehicle, distracted_driving, roadway_departure, month,
                    hour
            };
            int[] dimensions = new int[] { 1, 18 };
            return new DenseTensor<float>(data, dimensions);
        }
    }
}