# 17Track API Information

How to use:
1)send up to 40 tracking number as an array of string.
2)You will get back a List with each element containing information about each tracking number
3)info class will be an array where each element is an event during the shipping process.

            var test = new string[] { "Tracking_Number_Array" };
            var updates = new Results(test).results();
            foreach (var update in updates)
                foreach (var tmp in update.info)
                {
                    Console.WriteLine(tmp.Date);
                    Console.WriteLine(tmp.Location);
                    Console.WriteLine(tmp.Message);
                    Console.WriteLine();
                }
           
