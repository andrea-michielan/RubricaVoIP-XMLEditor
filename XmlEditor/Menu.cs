using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlEditor {
	class Menu {

		//Ogni menu ha un nome e un insieme di contatti
		private string name;
		private List<Contatto> contatti;

		//Ogni menu viene inizializzato con un nome, mentre la lista inizialmente è vuota
		public Menu(string name) {
			this.name = name;
			this.contatti = new List<Contatto>();
		}

		//Aggiunta di un elemento alla lista dei contatti
		public void add(Contatto c) {
			this.contatti.Add(c);
		}

		//Ottengo l'elemento con indice index dalla lista
		public Contatto get(int index) {
			return this.contatti[index];
		}

		//Quantità di elementi della lista
		public int size() {
			return this.contatti.Count;
		}

		//Metodo per il nome del menu
		public string getName() {
			return this.name;
		}

		//Metodo per pulire la lista di contatti
		public void clear() {
			this.contatti.Clear();
		}

		//Metodo per rimuovere un contatto dalla lista
		public void remove(int index) {
			this.contatti.RemoveAt(index);
		}
	}
}
