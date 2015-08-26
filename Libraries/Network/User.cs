using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public class User
{
    public IPEndPoint endpoint;
    public string name;

    public User(IPEndPoint endpoint, string name)
    {
        this.endpoint = endpoint;
        this.name = name;
    }

    public override string ToString()
    {
        return this.name;
    }

    public override bool Equals(object obj)
    {
        if (obj is IPEndPoint)
            return obj.ToString().Equals(endpoint.ToString());
        else if (obj is User)
                return ((User)obj).endpoint.ToString().Equals(endpoint.ToString());

        return base.Equals(obj);
    }
}