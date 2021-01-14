using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class ClientPlugin : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public ClientPlugin()
    {
      if (statDictionary == null)
      {
        statDictionary = new Dictionary<String, int>();

      }
    }

    public void PreProcessing(HTTPRequest request)
    {
      if (statDictionary.ContainsKey(request.Url))
      {
        statDictionary[request.Url] = (int)statDictionary[request.Url] + 1;
      }
      else
      {
        statDictionary[request.Url] = 1;
      }
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();

      //Create Array to stored data and split it.
      string[] Client = request.getPropertyByKey("RemoteEndPoint").Split(":");
      
      sb.Append("<html><body><h1>Client Information</h1>");
      sb.Append("<html><body>Client IP Address: " + Client[0] + "<br>"); //Show the IP address's client.
      sb.Append("<html><body>Client Port: " + Client[1] + "<br>"); //Show the IP address's client.
      sb.Append("<html><body>Browser Information: " + request.getPropertyByKey("user-agent")+ "<br>"); //Show the Browser Information's client.
      sb.Append("<html><body>Accept Language: " + request.getPropertyByKey("accept-language")+ "<br>"); //Show the Accept Language's client.
      sb.Append("<html><body>Accept Encoding: " + request.getPropertyByKey("accept-encoding")); //Show the Accept Encoding's client.
      sb.Append("</body></html>");
      
      response = new HTTPResponse(200);
      response.body = Encoding.UTF8.GetBytes(sb.ToString());
      return response;
    }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
  }
}