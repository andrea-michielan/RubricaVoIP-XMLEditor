using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlEditor {
	class Contatto {

		/*
		 * Ogni contatto ha un nome, 3 numeri di telefono e un'immagine di default (non obbligatoria)
		 * che se non ha alcun contenuto allora all'interno del tag html conterrà la scritta "Resource:"
		 */
		private string name;
		private string phone1;
		private string phone2;
		private string phone3;
		private string defaultImage;

		//Ciascun costruttore dovrà avere obbligatoriamente il nome e almeno 1 numero di telefono
		public Contatto(string name, string phone1) {
			this.name = name;
			this.phone1 = phone1;
			this.phone2 = "";
			this.phone3 = "";
			this.defaultImage = "Resource:";
		}

		//Costruttore per 2 numeri di telefono
		public Contatto(string name, string phone1, string phone2) {
			this.name = name;
			this.phone1 = phone1;
			this.phone2 = phone2;
			this.phone3 = "";
			this.defaultImage = "Resource:";
		}

		//Costruttore con  3 numeri di telefono, quello usato per comodità anche se non sono da inserire tutti e 3 i numeri
		public Contatto(string name, string phone1, string phone2, string phone3) {
			this.name = name;
			this.phone1 = phone1;
			this.phone2 = phone2;
			this.phone3 = phone3;
			this.defaultImage = "Resource:";
		}

		//Get del nome
		public string getName() {
			return this.name;
		}

		//Get del numero di telefono 1
		public string getPhone1() {
			return this.phone1;
		}

		//Get del numero di telefono 2
		public string getPhone2() {
			return this.phone2;
		}

		//Get del numero di telefono 3
		public string getPhone3() {
			return this.phone3;
		}

		//Set del nome
		public void setName(string name) {
			this.name = name;
		}

		//Set del numero di telefono 1
		public void setPhone1(string phone1) {
			this.phone1 = phone1;
		}

		//Set del numero di telefono 2
		public void setPhone2(string phone2) {
			this.phone2 = phone2;
		}

		//Set del numero di telefono 3
		public void setPhone3(string phone3) {
			this.phone3 = phone3;
		}

		//Serve solamente per rimuovere l'avviso che da visual studio
		public string getDefaultImage() {
			return this.defaultImage;
		}

	}
}
