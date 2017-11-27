using PLS.SKS.ServiceAgents.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLS.SKS.ServiceAgents.Interfaces
{
	public interface IGeoEncodingAgent
	{
		GeoCoordinates EncodeAddress(Address a);
	}
}
