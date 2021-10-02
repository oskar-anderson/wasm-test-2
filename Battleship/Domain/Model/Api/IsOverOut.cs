using RogueSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Api
{
	public class IsOverOut
	{
		public bool IsOver { get; set; }
		public string Winner { get; set; } = "";
	}
}
