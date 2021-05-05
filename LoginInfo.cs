using System;
using System.Collections.Generic;
using System.Text;

namespace YetiSharp
{
    /// <summary>
    /// Main class for information that Yetiforce returns after user login.
    /// </summary>
    public class LoginInfo
    {
        public string status { get; set; }
        public LoginResult result { get; set; }
        public LoginError error { get; set; }
    }

    public class LoginError
    {
        public string message { get; set; }
        public int code { get; set; }
        public string file { get; set; }
        public int line { get; set; }
        public string backtrace { get; set; }
    }

    public class Preferences
    {
        public string activity_view { get; set; }
        public int hour_format { get; set; }
        public string start_hour { get; set; }
        public string date_format { get; set; }
        public string date_format_js { get; set; }
        public string dayoftheweek { get; set; }
        public string time_zone { get; set; }
        public int currency_id { get; set; }
        public string currency_grouping_pattern { get; set; }
        public string currency_decimal_separator { get; set; }
        public string currency_grouping_separator { get; set; }
        public string currency_symbol_placement { get; set; }
        public int no_of_currency_decimals { get; set; }
        public int truncate_trailing_zeros { get; set; }
        public string end_hour { get; set; }
        public string currency_name { get; set; }
        public string currency_code { get; set; }
        public string currency_symbol { get; set; }
        public double conv_rate { get; set; }
    }

    public class LoginResult
    {
        public string token { get; set; }
        public string name { get; set; }
        public string parentName { get; set; }
        public string lastLoginTime { get; set; }
        public string lastLogoutTime { get; set; }
        public string language { get; set; }
        public int type { get; set; }
        public int companyId { get; set; }
        public object companyDetails { get; set; }
        public bool logged { get; set; }
        public Preferences preferences { get; set; }
    }
}
