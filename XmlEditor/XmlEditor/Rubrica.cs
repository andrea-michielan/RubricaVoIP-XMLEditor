using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlEditor {
	class Rubrica {

		//Ogni rubrica è costituita da più menu che a loro volta contengono più contatti
		private List<Menu> menuList;

		//Di default la rubrica non contiene nessun menu
		public Rubrica() {
			this.menuList = new List<Menu>();
		}

		//Metodo per ottenere il menu in posizione desiderata index
		public Menu get(int index) {
			return this.menuList[index];
		}

		//Metodo per la creazione di un nuovo menu
		public void add(string name) {
			this.menuList.Add(new Menu(name));
		}

		//Metodo per ottenere la quantità di menu presenti
		public int size() {
			return this.menuList.Count;
		}

		//Metodo per la rimozione di un menu dalla rubrica
		public void remove(int index) {
			this.menuList.RemoveAt(index);
		}

	}
}
