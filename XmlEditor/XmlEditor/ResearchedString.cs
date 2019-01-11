using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlEditor {
	class ResearchedString {

		//Oggetto statico che mantiene il suo contenuto tra tutte le classi 
		public static ResearchedString toSearch;

		//Quando viene effettuata una ricerca bisogna tenere conto della stringa da ricercare e del tipo di oggetto da ricercare
		private bool name;
		private string researchedString;

		//Metodo costruttore standard, nessuna particolarità
		public ResearchedString(bool name, string researchedString) {
			this.name = name;
			this.researchedString = researchedString;
		}

		//Set della stringa da ricercare
		public void setString(string researchedString) {
			this.researchedString = researchedString;
		}

		//Metodo per controllare se si sta ricercando per nome o per numero di telefono
		public bool isName() {
			return this.name;
		}

		//Get della stringa
		public string getString() {
			return this.researchedString;
		}

		//Set per comunicare se si ricerca per nome o per numero di telefono
		public void setName(bool name) {
			this.name = name;
		}

	}
}
