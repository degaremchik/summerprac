using Newtonsoft.Json;

namespace StudentGrades
{
    class Mark
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("last_name_ukr")]
        public string Lastname { get; set; }
        [JsonProperty("name_ukr")]
        public string Name { get; set; }
        [JsonProperty("group_number")]
        public string GroupNumber { get; set; }
        [JsonProperty("short_name")]
        public string Subject { get; set; }
        [JsonProperty("name")]
        public string Grade { get; set; }
        [JsonProperty("check_form")]
        public string CheckForm { get; set; }
        [JsonProperty("name_1")] 
        public string FormOfStudy { get; set; }
        [JsonProperty("last_name_ukr_1")]
        public string Lastname_Examiner { get; set; }
        [JsonProperty("name_ukr_1")]
        public string Name_Examiner { get; set; }
        [JsonProperty("chair_number")]
        public string ChairOfStudent { get; set; }
        [JsonProperty("chair_number_1")]
        public string ChairOfSubject { get; set; }
    }
}
