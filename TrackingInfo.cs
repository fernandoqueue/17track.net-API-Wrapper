using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TrackingInfo
{
      public class Results
    {
        static HttpClient client = new HttpClient();
        List<TrackingInformation> _results;

         public List<TrackingInformation> results { get { return _results; } }
        
        public Results(string[] tracklist)
        {      
            var jsonParams = GetJsonString(tracklist);
            var buffer = Encoding.UTF8.GetBytes(jsonParams);
            var byteContent = new ByteArrayContent(buffer);
            var response = client.PostAsync("https://t.17track.net/restapi/track", byteContent).Result;
            var  jsonTemp = JsonConvert.DeserializeObject<JsonResponse>(response.Content.ReadAsStringAsync().Result.ToString()).dat;
            _results = FilterResults(jsonTemp);
                  
        }
        private List<TrackingInformation> FilterResults(Dat[] json)
        {
            var list = new List<TrackingInformation>();
            foreach (var tmp in json)
            {
                var track = new TrackingInformation() { trackingnum = tmp.no, delay = tmp.delay };
                if (tmp.track != null)
                    track.info = tmp.track.z1;
                list.Add(track);
            }

            return list;
        }
        private string GetJsonString(string[] tracker)
        {
            var keyListArray = new KeyList[tracker.Count()];
            var parameters = new Paramaters() { data = keyListArray };
            for (int i = 0; i < keyListArray.Count(); i++)
                keyListArray[i] = new KeyList() { num = tracker[i] };
            return JsonConvert.SerializeObject(parameters);
        }

    }
    public class TrackingInformation
    {
        [JsonProperty(PropertyName = "no")]
        public string trackingnum { get; set; }
        public int delay { get; set; }
        public Z1[] info { get; set; }

    }
    public class KeyList
    {
        public string num { get; set; }
    }
    public class Paramaters
    {
        public string guid { get; set; } = "";
        public KeyList[] data { get; set; }

    }
    public class JsonResponse
    {
        public Dat[] dat { get; set; }
    }

    public class Dat
    {
        public string no { get; set; }
        public int delay { get; set; }
        public Track track { get; set; }
    }

    public class Track
    {
        public Z1[] z1 { get; set; }
    }
    public class Z1
    {
        [JsonProperty(PropertyName = "a")]
        public string Date { get; set; }
        [JsonProperty(PropertyName = "d")]
        public string Location { get; set; }
        [JsonProperty(PropertyName = "z")]
        public string Message { get; set; }
    }
}


