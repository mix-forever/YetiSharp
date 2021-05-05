using System;
using System.Net;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace YetiSharp
{
    public class YetiApi
    {
        /// <summary>
        /// WebService application user
        /// </summary>
        public static string ws_user ;
        /// <summary>
        /// WebService application password
        /// </summary>
        public static string ws_pass ;
        /// <summary>
        /// API key from YetiForse CRM
        /// </summary>
        public static string ws_key ;
        /// <summary>
        /// application username
        /// </summary>
        public static string username ;
        /// <summary>
        /// application password
        /// </summary>
        public static string password ;
        /// <summary>
        /// format: http://hostname_or_ip_address
        /// </summary>
        public static string host ;
        /// <summary>
        /// Is user loged in?
        /// </summary>
        public static bool isLogedIn = false ;

        public static RestClient Client;
        public static Dictionary<string, string> headers;

        /// <summary>
        /// Logs user into the system. No arguments needed.
        /// </summary>
        /// <returns></returns>
        public static LoginInfo Login()
        {
            headers = new Dictionary<string, string>();

            headers.Add("Accept", "application/json");
            headers.Add("User-Agent", "yeti-rest-api/1.0");
            headers.Add("X-ENCRYPTED", "0");
            headers.Add("X-API-KEY", ws_key);
            if (host.Contains ("http://") == false)
                host = "http://" + host;
            Client = new RestClient(host);
            Client.Authenticator = new HttpBasicAuthenticator(ws_user, ws_pass);
            Client.CookieContainer = new CookieContainer();

            var request = new RestRequest(Method.POST);
            request.Resource = "/webservice/Users/Login";
            request.AddHeaders(headers);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("userName", username);
            request.AddParameter("password", password);
            var response = Client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            int numStatCode = (int)statusCode;
            var info = JsonConvert.DeserializeObject<LoginInfo>(response.Content);
            if (numStatCode == 200)
                headers.Add("X-TOKEN", info.result.token);
                isLogedIn = true;
            return info;
        }

        /// <summary>
        /// Logout user out the system. No arguments needed.
        /// </summary>
        /// <returns></returns>
        public static ResultInfo Logout()
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "/webservice/Users/Logout";
            request.AddHeaders(headers);
            var response = Client.Execute(request);
            var info = JsonConvert.DeserializeObject<ResultInfo>(response.Content);
            return info;
        }

        /// <summary>
        /// Get the list of records from specyfic module.
        /// </summary>
        /// <param name="Module"></param>
        /// <param name="Fields"></param>
        /// <param name="RowLimit"></param>
        /// <param name="OffSet"></param>
        /// <returns></returns>
        public static YResponse GetRecordList(string Module, string[] Fields, int RowLimit = 1000, int OffSet = 0)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "/webservice/{moduleName}/RecordsList";
            request.AddHeaders(headers);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("moduleName", Module, ParameterType.UrlSegment);
            request.AddParameter("X-ROW-LIMIT", RowLimit, ParameterType.HttpHeader);
            request.AddParameter("X-ROW-OFFSET", OffSet, ParameterType.HttpHeader);
            request.AddParameter("X-FIELDS", JsonConvert.SerializeObject(Fields), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            var records = JsonConvert.DeserializeObject<YResponse>(response.Content);
            return records;
        }

        /// <summary>
        /// Get the related list of records
        /// </summary>
        /// <param name="Module"></param>
        /// <param name="recordId"></param>
        /// <param name="relatedModule"></param>
        /// <param name="Fields"></param>
        /// <param name="rawData"></param>
        /// <param name="RowLimit"></param>
        /// <param name="OffSet"></param>
        /// <returns></returns>
        public static YResponse GetRecordRelatedList(string Module, int recordId, string relatedModule, string[] Fields, int rawData=0, int RowLimit = 1000, int OffSet = 0)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "/webservice/{moduleName}/RecordRelatedList/{recordId}/{relatedModuleName}";
            request.AddHeaders(headers);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("moduleName", Module, ParameterType.UrlSegment);
            request.AddParameter("recordId", recordId, ParameterType.UrlSegment);
            request.AddParameter("relatedModuleName", relatedModule, ParameterType.UrlSegment);
            request.AddParameter("X-ROW-LIMIT", RowLimit, ParameterType.HttpHeader);
            request.AddParameter("X-ROW-OFFSET", OffSet, ParameterType.HttpHeader);
            request.AddParameter("X-FIELDS", JsonConvert.SerializeObject(Fields), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            var content = JsonConvert.DeserializeObject<YResponse> (response.Content);
            return content;
        }

        /// <summary>
        /// Edit record
        /// </summary>
        /// <param name="Module"></param>
        /// <param name="recordId"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static RecordInfo EditRecord(string Module, int recordId, object Data)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "/webservice/{moduleName}/Record/{recordId}";
            request.AddHeaders(headers);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("moduleName", Module, ParameterType.UrlSegment);
            request.AddParameter("recordId", recordId, ParameterType.UrlSegment);
            request.AddJsonBody(JsonConvert.SerializeObject(Data));
            IRestResponse response = Client.Execute(request);
            var records = JsonConvert.DeserializeObject<RecordInfo>(response.Content);
            return records;
        }

        /// <summary>
        /// Create new record
        /// </summary>
        /// <param name="Module"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static RecordInfo CreateRecord(string Module, object Data)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "/webservice/{moduleName}/Record";
            request.AddHeaders(headers);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("moduleName", Module, ParameterType.UrlSegment);
            request.AddJsonBody(JsonConvert.SerializeObject(Data));
            IRestResponse response = Client.Execute(request);
            var records = JsonConvert.DeserializeObject<RecordInfo>(response.Content);
            return records;
        }
        /// <summary>
        /// Delete record (move to the trash)
        /// </summary>
        /// <param name="Module"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static ResultInfo DeleteRecord(string Module, int recordId)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "/webservice/{moduleName}/Record/{recordId}";
            request.AddHeaders(headers);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("moduleName", Module, ParameterType.UrlSegment);
            request.AddParameter("recordId", recordId, ParameterType.UrlSegment);
            IRestResponse response = Client.Execute(request);
            var records = JsonConvert.DeserializeObject<ResultInfo>(response.Content);
            return records;
        }
    }
}
