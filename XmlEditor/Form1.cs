using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using System.Web;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace XmlEditor {
	public partial class Form1 : Form {

		//Variabili che mi permettono di eliminare in maniera corretta un intero menu
		int lastPhonebookSize = 0;

		//Variabile che mi permette di tenere traccia dell'ultimo indice selezionato dalla combobox
		int lastIndexChanged = 0;

		//Creo una chiave di registro sotto HKEY_CURRENT_USER
		RegistryKey phoneBookEditor;

		//Creo un'altra chiave sotto HKEY_CURRENT_USER\PhoneBookEditor
		RegistryKey fileSavePath;

		//Array bidimensionale dove sono contenuti gli elementi trovati con la ricerca
		List<List<int>> index;
		//Ultimo menu mostrato tra quelli ricercati
		int lastIndex = 0;

		//Oggetto riga utile per scorrere la tabella
		DataGridViewRow row;

		//Oggetto rubrica contenente tutte le informazioni necessarie per la creazione del file XML
		Rubrica phoneBook = new Rubrica();

		//Boolean per verificare se è già avvenuto il salvataggio con nome
		bool salvaConNome = false;

		//Valori che mi permettono di impedire l'inserimento errato di dati nelle celle
		int currentRow;
		bool insError = false;

		//Indici per scorrere gli oggetti
		int i = 0;
		int l = 0;

		public Form1() {
			InitializeComponent();
		}

		//Azioni svolte al caricamento della finestra principale
		private void Form1_Load(object sender, EventArgs e) {

			//Modifico il titolo della finestra
			this.Text = "Editor Rubrica - Nuovo file";

			//Inizializzo le chiavi di registro
			phoneBookEditor = Registry.CurrentUser.CreateSubKey(@"Software\PhoneBookEditor", RegistryKeyPermissionCheck.ReadWriteSubTree);
			fileSavePath = phoneBookEditor.CreateSubKey("FileSavePath", RegistryKeyPermissionCheck.ReadWriteSubTree);

			//All'apertura della finestra carico il percorso di salvataggio/apertura di default
			try {

				//Modifica della chiave di registro contenente il percorso di salvataggio del file
				using (RegistryKey tempKey = phoneBookEditor.OpenSubKey("FileSavePath", RegistryKeyPermissionCheck.ReadWriteSubTree)) {
					XmlFile.file = @"" + tempKey.GetValue("Path").ToString();
				}

			} catch (NullReferenceException ex) {

				//Le chiavi vengono chiuse automaticamente quando l'esecuzione del programma esce dal costrutto "using"
				using (fileSavePath) {
					//Inserisco la stringa del percorso di salvataggio all'interno della chiave di registro
					fileSavePath.SetValue("Path", @"C:\Users\*\Documents");
				}

				//Rileggo il nuovo percorso dalla chiave di registro appena modificata
				using (RegistryKey tempKey = phoneBookEditor.OpenSubKey("FileSavePath", RegistryKeyPermissionCheck.ReadWriteSubTree)) {
					XmlFile.file = @"" + tempKey.GetValue("Path").ToString();
				}
			}

			//Apertura e salvataggio dei file sono di tipo XML
			openFileDialog1.Filter = "File XML (*.xml)|*.xml";
			saveFileDialog1.Filter = "File XML (*.xml)|*.xml";

			//Il combobox funziona come un menu a tendina, quindi non è impossibile modificare il campo
			menuComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

			//Ascoltatori della tabella
			dataGridView1.Click += new EventHandler(dataGridView1_Click);
			dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
			dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
			dataGridView1.UserDeletingRow += new DataGridViewRowCancelEventHandler(dataGridView1_UserDeletingRow);
			dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView1_RowPostPaint);

			//DoubleBuffering per il DataGridView
			typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
				BindingFlags.Instance | BindingFlags.SetProperty, null,
				dataGridView1, new object[] { true });

			//Alla pressione del tasto invio viene premuto il tasto aggiungi
			this.AcceptButton = addMenuButton;

			//Indica che la finestra corrente contiene altre finestre
			this.IsMdiContainer = true;

			//Pressione di un bottone mentre si è nella finestra principale
			this.KeyDown += new KeyEventHandler(Form1_KeyDown);

			//Serve per poter creare delle shortcuts invece che premere sui bottoni
			this.KeyPreview = true;

			//Ascoltatore chiusura in corso finestra
			this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);

			//Seleziono uno tra i due radiobutton di ricerca
			nameRadioButton.Checked = true;
			phoneNumberRadioButton.Checked = false;

			//Inizialmente i bottoni precedente e successivo sono disattivati
			nextButton.Enabled = false;
			previousButton.Enabled = false;

		}

		//Numero della riga
		void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e) {
			dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
		}

		//Ascoltatore nel caso in cui vengano eliminate delle righe dalla tabella (contatti)
		void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) {

			//var watch = System.Diagnostics.Stopwatch.StartNew();

			//Rimuovo il contatto anche dalla rubrica come oggetto e non solo dalla tabella di visualizzazione
			if (phoneBook.size() > 0) {
				if (phoneBook.get(0).size() > 0) {
					phoneBook.get(menuComboBox.SelectedIndex).remove(Int32.Parse(e.Row.Index.ToString()));
				}
			}

			dataGridView1.Rows[e.Row.Index + 1].Selected = true;

			//watch.Stop();
			//label2.Text = watch.ElapsedMilliseconds.ToString();

		}

		//Se bisogna correggere dei dati appena inseriti faccio ritornare l'utente alla cella da correggere
		void dataGridView1_SelectionChanged(object sender, EventArgs e) {

			//Controllo se c'è stato un errore di inserimento
			if (insError) {

				//Rimetto in modalità modifica la cella
				dataGridView1.CurrentCell = dataGridView1[1, currentRow];

				dataGridView1.BeginEdit(true);

			}

		}

		//Appena viene completato l'inserimento dei dati in una cella eseguo dei controlli sui dati inseriti
		void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e) {

			//Controllo se è presente almeno un menu, in caso contrario riferisco l'errore
			if (phoneBook.size() == 0) {
				MessageBox.Show("Prima creare un nuovo menu!", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
				clearAll();
			} else {

				string ss = "";

				try {
					ss = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
				} catch (NullReferenceException ne) {
					ss = "";
				}

				//Controllo che l'utente abbia assegnato un nome al contatto
				if (string.IsNullOrWhiteSpace(ss)) {

					MessageBox.Show("Il campo Nome è obbligatorio e deve contenere almeno un carattere diverso dallo spazio!", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					insError = true;
					currentRow = e.RowIndex;

				} else {

					insError = false;

				}

			}

		}

		//Apertura di una finestra di dialogo che chiederà se si vuole salvare il lavoro corrente
		void Form1_FormClosing(object sender, FormClosingEventArgs e) {

			//Creazione della finestra di dialogo e visualizzazione
			switch (MessageBox.Show("Si è sicuri di voler terminare l'esecuzione del programma?\nTutti i progressi non salvati andranno persi!", "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {

				case DialogResult.Yes:
					break;

				case DialogResult.No:
					e.Cancel = true;
					break;

				default:
					break;

			}

		}

		//Controllo che tasti sono stati premuti e eseguo i comandi assegnati
		void Form1_KeyDown(object sender, KeyEventArgs e) {

			//ShortCuts per accedere rapidamente da tastiera alle icone della barra degli strumenti
			if (e.KeyCode == Keys.S && e.Control) {
				salvaToolStripButton_Click(null, null);
			} else if (e.KeyCode == Keys.O && e.Control) {
				apriToolStripButton_Click(null, null);
			} else if (e.KeyCode == Keys.N && e.Control) {
				toolStripButton1_Click(null, null);
			} else if (e.KeyCode == Keys.F12 && e.Control) {
				toolStripButton2_Click(null, null);
			}

		}

		//Click sulla tabella
		void dataGridView1_Click(object sender, EventArgs e) {

			//Controllo se è presente almeno un menu, in caso contrario riferisco l'errore
			if (phoneBook.size() == 0) {
				MessageBox.Show("Prima creare un nuovo menu!", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
				clearAll();
			}

		}

		//Apertura di una nuova finestra per la scelta del file
		private void apriToolStripButton_Click(object sender, EventArgs e) {

			//Apro la finestra per l'apertura del file
			openFileDialog1.ShowDialog();

		}

		//Salvataggio del file senza finestra di dialogo
		private void salvaToolStripButton_Click(object sender, EventArgs e) {

			//Innanzitutto controllo che ci sia almeno un menu con almeno un contatto
			if (dataGridView1.Rows.Count > 1) {

				//Controllo se è stato già effettuato un salvataggio
				if (salvaConNome) {

					//Richiamo il metodo per il salvataggio del file senza passare per la finestra di dialogo
					saveFileDialog1_FileOk(null, null);

				} else {

					//Se non è mai stato scelto con che nome salvare il file allora apro la finestra di dialogo
					toolStripButton2_Click(null, null);

				}

			} else {

				//Se non ci sono contatti allora lo comunico con un messaggio di errore
				MessageBox.Show("Per effettuare il salvataggio è necessario creare almeno un menu costituito da almeno un contatto!", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Error);

			}

		}

		//Operazioni che vengono effettuate all'avvenuta apertura di una rubrica da file XML
		private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {

			//Inizialmente i bottoni precedente e successivo sono disattivati
			nextButton.Enabled = false;
			previousButton.Enabled = false;
			label2.Text = "";

			//All'apertura di un file pulisco prima la tabella e la combobox 
			//per evitare che i nuovi dati si aggiungano a quelli di un file
			//aperto in precedenza
			dataGridView1.Rows.Clear();
			phoneBook = new Rubrica();
			menuComboBox.Items.Clear();

			//Creo un nuovo documento xml
			XmlDocument xmlDoc = new XmlDocument();

			//Ottengo il file scelto in apertura
			XmlFile.file = openFileDialog1.FileName;

			//Modifico il titolo della finestra
			this.Text = "Editor Rubrica - " + XmlFile.file;

			//Modifico il percorso di salvataggio di default
			using (RegistryKey tempKey = phoneBookEditor.OpenSubKey("FileSavePath", RegistryKeyPermissionCheck.ReadWriteSubTree)) {
				//Inserisco la stringa del percorso di salvataggio all'interno della chiave di registro
				tempKey.SetValue("Path", @XmlFile.file);
			}

			//Carico il file XML scelto
			xmlDoc.Load(@XmlFile.file);

			//Creazione lista dei nodi dei menu
			XmlNodeList menuList = xmlDoc.SelectNodes("/YealinkIPPhoneBook/Menu");

			//Scorro tutti i nodi menu e li aggiungo alla lista di menu (Rubrica)
			foreach (XmlNode node in menuList) {
				phoneBook.add(node.Attributes["Name"].Value);
				//Aggiungo i vari menu al menu a tendina
				menuComboBox.Items.Add(node.Attributes["Name"].Value);
			}

			//Lista dei nodi a partire dal percorso indicato
			XmlNodeList unitList = xmlDoc.SelectNodes("/YealinkIPPhoneBook/Menu/Unit");

			i = 0;
			l = 0;

			//Stringhe temporanee per controllare se devo scorrere la lista dei menu
			string temp = "";
			string temp2 = "";
			temp = phoneBook.get(l).getName();

			//Intero utile per riprendere a scorrere la lista dei contatti da dove mi ero fermato
			int util = 0;

			//Scorro tutti i menu presenti nella rubrica
			for (i = 0; i < phoneBook.size(); i++) {

				//Scorro tutti i contatti del menu corrente
				for (l = util; l < unitList.Count; l++) {

					//Aggiungo i contatti al menu corrente
					phoneBook.get(i).add(new Contatto(/*HttpUtility.HtmlDecode*/(unitList.Item(l).Attributes["Name"].Value),
													unitList.Item(l).Attributes["Phone1"].Value,
													unitList.Item(l).Attributes["Phone2"].Value,
													unitList.Item(l).Attributes["Phone3"].Value));


					//Controllo se ho raggiunto la fine dei menu
					if ((i < (phoneBook.size() - 1)) && (phoneBook.size() > 1)) {

						//Ottengo il nome del menu del contatto corrente e di quello successivo
						temp = unitList.Item(l).ParentNode.Attributes["Name"].Value;
						temp2 = unitList.Item(l + 1).ParentNode.Attributes["Name"].Value;

						//Se il nome del menu è diverso tra i 2 allora passo al menu successivo
						if (!temp.Equals(temp2)) {
							break;
						}

					}

				}

				//Salvo l'indice della lista da dove mi ero fermato per poterla poi riprendere
				util = l + 1;

			}

			//Carico tutti i dati nel programma
			showUnits(0);

			//Seleziono nel menu a tendina il primo menu disponibile e visualizzo il suo contenuto
			menuComboBox.SelectedIndex = 0;

			//Conferma apertura
			MessageBox.Show("Apertura completata!", "Completato", MessageBoxButtons.OK, MessageBoxIcon.Information);

		}

		//Operazioni che vengono effettuate per salvare la rubrica in un file XML
		private void saveFileDialog1_FileOk(object sender, CancelEventArgs e) {

			//Indico che è stato richiesto il salvataggio con nome
			salvaConNome = true;

			//Salvo sull'oggetto rubrica i dati del menu corrente visualizzato sulla tabella
			tableToPhoneBook(menuComboBox.SelectedIndex);

			//Ottengo il file scelto per il salvataggio
			XmlFile.file = saveFileDialog1.FileName;

			//Modifico il percorso di salvataggio di default
			using (RegistryKey tempKey = phoneBookEditor.OpenSubKey("FileSavePath", RegistryKeyPermissionCheck.ReadWriteSubTree)) {
				//Inserisco la stringa del percorso di salvataggio all'interno della chiave di registro
				tempKey.SetValue("Path", @XmlFile.file);
			}

			//Creazione di un nuovo file XML
			XmlDocument xmlDoc = new XmlDocument();

			//Creazione nodo di intestazione
			XmlNode headerNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
			xmlDoc.AppendChild(headerNode);

			//Creazione del nodo radice e aggiunta al documento XML
			XmlNode rootNode = xmlDoc.CreateElement("YealinkIPPhoneBook");
			xmlDoc.AppendChild(rootNode);

			//Creazione nodo titolo
			XmlNode titleNode = xmlDoc.CreateElement("Title");
			titleNode.InnerText = "Yealink";
			rootNode.AppendChild(titleNode);

			//Vado a prendere i dati dall'oggetto Rubrica contenente tutti i dati runtime
			//Scorro tutti i menu della rubrica
			for (i = 0; i < phoneBook.size(); i++) {

				//Creo il menu
				XmlNode menuNode = xmlDoc.CreateElement("Menu");
				//Attributo nodo menu (nome del menu)
				XmlAttribute menuAttribute = xmlDoc.CreateAttribute("Name");
				menuAttribute.InnerXml = phoneBook.get(i).getName();
				//Aggiungo l'attributo al nodo
				menuNode.Attributes.Append(menuAttribute);
				//Aggiungo il nodo al nodo padre
				rootNode.AppendChild(menuNode);

				//Scorro tutti i contatti del menu corrente
				for (l = 0; l < phoneBook.get(i).size(); l++) {

					//Creazione nodo contatto
					XmlNode unitNode = xmlDoc.CreateElement("Unit");

					//Attributo nome del nodo
					XmlAttribute nameAttribute = xmlDoc.CreateAttribute("Name");
					//Contenuto attributo nome del nodo
					try {
						nameAttribute.InnerXml = HttpUtility.HtmlEncode(phoneBook.get(i).get(l).getName());
					} catch (XmlException xmlEx) {
						//Visualizzazione messaggio di errore
						MessageBox.Show(xmlEx.ToString(), "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					//Aggiungo l'attributo al nodo
					unitNode.Attributes.Append(nameAttribute);

					//Attributo telefono1 del nodo
					XmlAttribute phone1Attribute = xmlDoc.CreateAttribute("Phone1");
					//Contenuto attributo nome del nodo
					phone1Attribute.InnerXml = phoneBook.get(i).get(l).getPhone1();
					//Aggiungo l'attributo al nodo
					unitNode.Attributes.Append(phone1Attribute);

					//Attributo telefono2 del nodo
					XmlAttribute phone2Attribute = xmlDoc.CreateAttribute("Phone2");
					//Contenuto attributo nome del nodo
					phone2Attribute.InnerXml = phoneBook.get(i).get(l).getPhone2();
					//Aggiungo l'attributo al nodo
					unitNode.Attributes.Append(phone2Attribute);

					//Attributo telefono1 del nodo
					XmlAttribute phone3Attribute = xmlDoc.CreateAttribute("Phone3");
					//Contenuto attributo nome del nodo
					phone3Attribute.InnerXml = phoneBook.get(i).get(l).getPhone3();
					//Aggiungo l'attributo al nodo
					unitNode.Attributes.Append(phone3Attribute);

					//Attributo immagine del nodo
					XmlAttribute imageAttribute = xmlDoc.CreateAttribute("default_photo");
					//Contenuto attributo nome del nodo
					imageAttribute.InnerXml = "Resource:";
					//Aggiungo l'attributo al nodo
					unitNode.Attributes.Append(imageAttribute);

					//Aggiungo il nodo contatto al nodo menu
					menuNode.AppendChild(unitNode);

				}

			}

			//Salvataggio del file
			xmlDoc.Save(@XmlFile.file);

			//Conferma salvataggio
			MessageBox.Show("Salvataggio effettuato!", "Conferma", MessageBoxButtons.OK, MessageBoxIcon.Information);

		}

		//Ogni qual volta l'utente sceglie un elemento diverso dalla combobox
		//la tabella viene aggiornata con i valori corrispondenti al menu scelto
		//Inoltre viene aggiornata la rubrica per permettere poi il suo salvataggio
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {

			//Controllo se l'indice corrente e quello precedente sono diversi
			 if (lastIndexChanged != menuComboBox.SelectedIndex) {

				//Controllo che sia presente almeno un contatto nel menu corrente prima di effettuare il cambiamento
				if (dataGridView1.Rows.Count < 2) {

					//Controllo che sia presente almeno un altro menu
					if (phoneBook.size() > 1) {

						//Riporto l'utente al menu senza contatti
						menuComboBox.SelectedIndex = lastIndexChanged;

						//Comunico l'errore
						MessageBox.Show("Prima inserire almeno un contatto per il menu corrente oppure eliminare l'intero menu!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);

						showUnits(menuComboBox.SelectedIndex);

					}

				} else {

					//Controllo se è stato eliminato un menu
					if (lastPhonebookSize >= phoneBook.size()) {

						//Se è stato eliminato allora non salvo i dati della tabella del menu precedente
						

					} else {

						//Solo se è già presente un altro menu allora eseguo il salvataggio dei dati
						if (phoneBook.size() > 1) {

							//Richiamo il metodo per salvare gli oggetti del menu precedente
							tableToPhoneBook(lastIndexChanged);

						}

					}

					//Visualizzo sulla tabella i contatti del menu selezionato
					dataGridView1.Rows.Clear();
					showUnits(menuComboBox.SelectedIndex);

					//Salvo l'ultimo indice utilizzato della combobox
					lastIndexChanged = menuComboBox.SelectedIndex;

				}

			}

		}

		//Metodo che inserisce e visualizza i contatti all'interno della tabella
		private void showUnits(int index) {

			//Pulisco la tabella di contatti
			dataGridView1.Rows.Clear();

			progressBar1.Maximum = phoneBook.get(index).size();
			progressBar1.Step = 1;
			progressBar1.Value = 0;

			//Inserisco gli elementi copiati nella tabella di visualizzazione scorrendo i contatti di ciasun menu
			for (i = 0; i < phoneBook.get(index).size(); i++) {
				//Creo una nuova riga
				row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
				//Valore della prima colonna (colonna dei numeri)
				//row.Cells[0].Value = (i + 1);
				//Colonna del nome
				row.Cells[1].Value = HttpUtility.HtmlDecode(phoneBook.get(index).get(i).getName());
				//Colonna telefono 1
				row.Cells[2].Value = phoneBook.get(index).get(i).getPhone1();
				//Colonna telefono 2
				row.Cells[3].Value = phoneBook.get(index).get(i).getPhone2();
				//Colonna telefono 3
				row.Cells[4].Value = phoneBook.get(index).get(i).getPhone3();
				//Aggiunta della riga alla tabella
				dataGridView1.Rows.Add(row);
				progressBar1.PerformStep();
			}

		}

		//Aggiunta di un nuovo menu
		private void button1_Click(object sender, EventArgs e) {

			//Innanzitutto controllo se sono già presenti altri menu
			if (phoneBook.size() > 0) {

				//Poi controllo che l'utente abbia inserito almeno un contatto nel menu corrente
				bool b = false;

				//Scorro tutti i menu per controllare che su tutti ci sia almeno un contatto
				for (i = 0; i < phoneBook.size(); i++) {

					//Se non ci sono elementi in questo menu allora esco dal ciclo e lo comunico all'utente
					if (dataGridView1.Rows.Count == 1) {

						b = true;
						break;

					}

				}

				//Comunico all'utente e non gli permetto di aggiungere un menu
				if (b) {

					//Messaggio di errore
					MessageBox.Show("Inserire almeno un contatto per Menu!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

				} else {

					//Controllo che il nome del menu non sia vuoto
					if (!string.IsNullOrWhiteSpace(newMenuNameTextBox.Text)) {

						if (phoneBook.size() > 0) {
							phoneBook.get(menuComboBox.SelectedIndex).clear();
						}

						//Controllo che ci sia almeno una riga nella tabella
						if (dataGridView1.Rows.Count - 1 > 0) {

							//Salvo tutti i contatti del menu corrente nel menu corrispondente
							for (int i = 0; i < dataGridView1.Rows.Count - 1; i++) {

								string[] str = new string[] { "", "", "", "" };

								//Scorro le 4 colonne della tabella
								for (int l = 0; l < 4; l++) {

									try {
										str[l] = (string)dataGridView1.Rows[i].Cells[l + 1].Value.ToString();
									} catch (NullReferenceException exc) { }

								}

								phoneBook.get(menuComboBox.SelectedIndex).add(new Contatto(HttpUtility.HtmlEncode(str[0]), str[1], str[2], str[3]));

							}

						}

						//Creo un nuovo menu
						phoneBook.add(newMenuNameTextBox.Text);

						//Aggiungo un nuovo menu al combobox
						menuComboBox.Items.Add(newMenuNameTextBox.Text);

						//Cambio il menu visualizzato nella tabella
						menuComboBox.SelectedIndex = phoneBook.size() - 1;
						showUnits(phoneBook.size() - 1);

						//Visualizzazione messaggio di conferma
						MessageBox.Show("Menu aggiunto correttamente!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

					} else {

						//Visualizzazione messaggio di errore
						MessageBox.Show("Il nome non può contenere solo spazi!", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);

					}

				}

			} else {

				//Controllo che il nome del menu non sia vuoto
				if (!string.IsNullOrWhiteSpace(newMenuNameTextBox.Text)) {

					if (phoneBook.size() > 0) {
						phoneBook.get(menuComboBox.SelectedIndex).clear();
					}

					//Controllo che ci sia almeno una riga nella tabella
					if (dataGridView1.Rows.Count - 1 > 0) {

						//Salvo tutti i contatti del menu corrente nel menu corrispondente
						for (int i = 0; i < dataGridView1.Rows.Count - 1; i++) {

							string[] str = new string[] { "", "", "", "" };

							//Scorro le 4 colonne della tabella
							for (int l = 0; l < 4; l++) {

								try {
									str[l] = (string)dataGridView1.Rows[i].Cells[l + 1].Value.ToString();
								} catch (NullReferenceException exc) {}

							}

							phoneBook.get(menuComboBox.SelectedIndex).add(new Contatto(HttpUtility.HtmlEncode(str[0]), str[1], str[2], str[3]));

						}

					}

					//Creo un nuovo menu
					phoneBook.add(newMenuNameTextBox.Text);

					//Aggiungo un nuovo menu al combobox
					menuComboBox.Items.Add(newMenuNameTextBox.Text);

					//Cambio il menu visualizzato nella tabella
					menuComboBox.SelectedIndex = phoneBook.size() - 1;
					showUnits(phoneBook.size() - 1);

					//Visualizzazione messaggio di conferma
					MessageBox.Show("Menu aggiunto correttamente!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

				} else {

					//Visualizzazione messaggio di errore
					MessageBox.Show("Il nome non può contenere solo spazi!", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);

				}

			}

			lastPhonebookSize = phoneBook.size();

		}

		//Quando viene selezionato il campo per l'aggiunta di un menu il bottone di invio premerà il bottone aggiungi
		private void textBox1_TextChanged(object sender, EventArgs e) {
			//Alla pressione del tasto invio viene premuto il tasto aggiungi
			this.AcceptButton = addMenuButton;
		}

		//Bottone elimina che rimuove un'intero menu
		private void button2_Click(object sender, EventArgs e) {

			//Chiedo prima conferma nel caso in cui il bottone fosse stato premuto per errore
			switch (MessageBox.Show("Si è sicuri di voler rimuovere l'intero Menu e quindi i suoi contatti?", "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {

				case DialogResult.Yes:

					//Controllo se sono presenti altri menu
					if (phoneBook.size() > 0) {

						//Rimozione del menu dalla rubrica (e quindi ricorsivamente di tutti i contatti)
						phoneBook.remove(menuComboBox.SelectedIndex);

						lastIndexChanged = 0;

						//Rimuovo il menu anche dalla combobox
						menuComboBox.Items.RemoveAt(menuComboBox.SelectedIndex);

						//Controllo se sono presenti altri menu
						if (phoneBook.size() > 0) {

							//Dopo aver rimosso il menu vado a cambiare il menu visualizzato
							menuComboBox.SelectedIndex = 0;

							showUnits(menuComboBox.SelectedIndex);

						} else {

							//Se non sono presenti altri elementi allora pulisco tutto quanto
							dataGridView1.Rows.Clear();
							menuComboBox.Items.Clear();

						}

					} else {

						//Visualizzazione messaggio di errore
						MessageBox.Show("Nessun menu esistente!", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);

						//Se non sono presenti altri elementi allora pulisco tutto quanto
						dataGridView1.Rows.Clear();
						menuComboBox.Items.Clear();

					}

					break;

				case DialogResult.No:
					break;

				default:
					break;

			}

		}

		//Metodo per pulire tutto il contenuto della tabella e della combobox
		private void clearAll() {

			//Pulizia tabella
			dataGridView1.Rows.Clear();

			//Pulizia combobox
			menuComboBox.Items.Clear();

		}

		//Funzione per ricercare un elemento (per nome o numero di telefono) nella rubrica
		private bool search() {

			//Flag per controllare se sono state trovate delle corrispondenze
			bool found = false;

			//Array bidimensionale che conterrà la posizione nella tabella di tutti gli elementi corrispondenti ai criteri
			index = new List<List<int>>();

			progressBar1.Maximum = phoneBook.size();
			progressBar1.Step = 1;

			//Scorro tutti i menu della rubrica
			for (i = 0; i < phoneBook.size(); i++) {

				//Scorro tutti i contatti del menu corrente
				for (l = 0; l < phoneBook.get(i).size(); l++) {

					//Controllo se devo cercare per nome o per numero di telefono
					if (ResearchedString.toSearch.isName()) {

						//Controllo se trovo una corrispondenza
						if (phoneBook.get(i).get(l).getName().IndexOf(ResearchedString.toSearch.getString(), StringComparison.OrdinalIgnoreCase) >= 0) {


							//Comunico che la ricerca è avvenuta con successo
							found = true;

							//Creo la seconda dimensione dell'array
							index.Add(new List<int>());

							//Salvo in che posizione è stato trovato l'oggetto
							index[index.Count - 1].Add(i); //Menu
							index[index.Count - 1].Add(l); //Riga del menu

						}

					} else {

						//Se invece devo cercare tra i numeri di telefono allora devo confrontare la stringa con tutti e 3 i campi telefonici
						if (ResearchedString.toSearch.getString().Equals(phoneBook.get(i).get(l).getPhone1()) ||
							ResearchedString.toSearch.getString().Equals(phoneBook.get(i).get(l).getPhone2()) ||
							ResearchedString.toSearch.getString().Equals(phoneBook.get(i).get(l).getPhone3())) {

							//Comunico che la ricerca è avvenuta con successo
							found = true;

							//Creo la seconda dimensione dell'array
							index.Add(new List<int>());

							//Salvo in che posizione è stato trovato l'oggetto
							index[index.Count - 1].Add(i); //Menu
							index[index.Count - 1].Add(l); //Riga del menu

							//Se il criterio di ricerca è il numero telefonico allora esco dal ciclo in quanto non possono
							//esserci più numeri di telefono corrispondenti
							break;

						}


					}


				}

				//Se l'oggetto è stato trovato almeno una volta e sto cercando per numero di telefono allora esco dal ciclo
				if (!ResearchedString.toSearch.isName() && found) {
					break;
				}

				progressBar1.PerformStep();

			}

			return found;

		}

		//Bottone per la creazione di un nuovo progetto
		private void toolStripButton1_Click(object sender, EventArgs e) {

			//Chiedo prima la conferma
			switch (MessageBox.Show("Si è sicuri di voler creare una nuova rubrica?\nTutti i progressi non salvati andranno persi!", "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {

				case DialogResult.No:
					break;

				case DialogResult.Yes:

					insError = false;

					//Modifico il titolo della finestra
					this.Text = "Editor Rubrica - Nuovo file";

					//Modifico anche il boolean di controllo salvaConNome
					salvaConNome = false;

					//Pulisco tutto quanto
					clearAll();
					phoneBook = new Rubrica();

					//Inizialmente i bottoni precedente e successivo sono disattivati
					nextButton.Enabled = false;
					previousButton.Enabled = false;
					label2.Text = "";

					break;

				default:
					break;
			}

		}

		//Bottone salvataggio di un file con nome
		private void toolStripButton2_Click(object sender, EventArgs e) {

			//Innanzitutto controllo che ci sia almeno un menu con almeno un contatto
			if (dataGridView1.Rows.Count > 1) {

				//Apro la finestra di dialogo per il salvataggio del file
				saveFileDialog1.ShowDialog();

			} else {

				//Se non ci sono contatti allora lo comunico con un messaggio di errore
				MessageBox.Show("Per effettuare il salvataggio è necessario creare almeno un menu costituito da almeno un contatto!", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Error);

			}

		}

		//Bottone ricerca contatti
		private void button3_Click(object sender, EventArgs e) {

			//Controllo che sia presente del testo nel textbox
			if (searchTextBox.Text == "") {

				MessageBox.Show("Campo di testo vuoto!", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);

			} else {

				tableToPhoneBook(menuComboBox.SelectedIndex);

				ResearchedString.toSearch = new ResearchedString(nameRadioButton.Checked, searchTextBox.Text);

				//Controllo se la ricerca ha avuto successo o meno
				if (!search()) {

					//Visualizzazione messaggio di informazione
					MessageBox.Show("Nessuna corrispondenza con i valori inseriti\nRiprovare con altri valori!", "Nessun risultato", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

				} else {

					//Controllo se è stato trovato più di un elemento
					if (index.Count > 1) {

						//Visualizzazione messaggio di successo
						MessageBox.Show(index.Count + " elementi trovati!", "Ricerca completata", MessageBoxButtons.OK, MessageBoxIcon.Information);

						//Se ci sono più riscontri i bottoni successivo e precedente non servono
						nextButton.Enabled = true;
						previousButton.Enabled = true;

						//Visualizzo un testo con le informazioni del numero di riscontri e del riscontro corrente
						label2.Text = "Riscontro 1/" + index.Count.ToString();

					} else {

						//Visualizzazione messaggio di successo
						MessageBox.Show("Un elemento trovato!", "Ricerca completata", MessageBoxButtons.OK, MessageBoxIcon.Information);

						//Se c'è un solo riscontro i bottoni successivo e precedente non servono
						nextButton.Enabled = false;
						previousButton.Enabled = false;

						//Visualizzo un testo con le informazioni del numero di riscontri e del riscontro corrente
						label2.Text = "Riscontro 1/" + index.Count.ToString();

					}

					//Se l'elemento viene trovato allora bisogna selezionare la riga corrispondente all'elemento trovato

					//Cambio il menu visualizzato con quello dove è presente l'oggetto trovato
					showUnits(index[0][0]);

					//Cambio anche il menu visualizzato sulla combobox
					menuComboBox.SelectedIndex = index[0][0];

					//Cambio la riga con quella dove si trova l'elemento
					dataGridView1.FirstDisplayedScrollingRowIndex = index[0][1];
					dataGridView1.Refresh();
					dataGridView1.Rows[index[0][1]].Selected = true;

				}

			}

		}

		//Bottone per scorrere in avanti la lista degli elementi trovati
		private void button4_Click(object sender, EventArgs e) {

			//Controllo se è presente un elemento successivo o se devo ricominciare
			if (index.Count - 1 > lastIndex) {

				//Se si allora passo all'oggetto successivo
				lastIndex++;

			} else {

				//Altrimenti ricomincio da zero
				lastIndex = 0;

			}

			//Modifico il numero di riscontro corrente
			label2.Text = "Riscontro " + (lastIndex + 1).ToString() + "/" + index.Count.ToString();

			//Cambio il menu visualizzato con quello dove è presente l'oggetto trovato
			showUnits(index[lastIndex][0]);

			//Cambio anche il menu visualizzato sulla combobox
			menuComboBox.SelectedIndex = index[lastIndex][0];

			//Cambio la riga con quella dove si trova l'elemento
			dataGridView1.FirstDisplayedScrollingRowIndex = index[lastIndex][1];
			dataGridView1.Rows[index[lastIndex][1]].Selected = true;

		}

		//Bottone per scorrere verso indietro la lista degli elementi trovati
		private void button5_Click(object sender, EventArgs e) {

			//Controllo se è presente un elemento successivo o se devo ricominciare
			if (lastIndex != 0) {

				//Se si allora passo all'oggetto precedente
				lastIndex--;

			} else {

				//Altrimenti ricomincio dall'ultimo elemento
				lastIndex = index.Count - 1;

			}

			//Modifico il numero di riscontro corrente
			label2.Text = "Riscontro " + (lastIndex + 1).ToString() + "/" + index.Count.ToString();

			//Cambio il menu visualizzato con quello dove è presente l'oggetto trovato
			showUnits(index[lastIndex][0]);

			//Cambio anche il menu visualizzato sulla combobox
			menuComboBox.SelectedIndex = index[lastIndex][0];

			//Cambio la riga con quella dove si trova l'elemento
			dataGridView1.FirstDisplayedScrollingRowIndex = index[lastIndex][1];
			dataGridView1.Rows[index[lastIndex][1]].Selected = true;

		}

		//Quando si comincia l'inserimento di testo nel campo di ricerca il bottone invio farà avviare la ricerca
		private void textBox2_TextChanged(object sender, EventArgs e) {
			//Alla pressione del tasto invio viene premuto il tasto aggiungi
			this.AcceptButton = searchButton;
		}

		//Metodo che salva su un menu dell'oggetto rubrica tutti i contatti visualizzati sulla tabella
		private void tableToPhoneBook(int index) {

			//Prima di visualizzare nella tabella il nuovo menu salvo tutti i contatti degli altri menu
			phoneBook.get(index).clear();

			//Salvo tutti i contatti del menu corrente nel menu corrispondente
			for (i = 0; i < dataGridView1.Rows.Count - 1; i++) {

				string[] str = new string[] { "", "", "", "" };

				//Scorro le 4 colonne della tabella
				for (int l = 0; l < 4; l++) {

					try {
						str[l] = (string)dataGridView1.Rows[i].Cells[l + 1].Value.ToString();
					} catch (NullReferenceException exc) {
						str[l] = "";
					}

				}

				if (!string.IsNullOrWhiteSpace(str[0])) {

					phoneBook.get(index).add(new Contatto(str[0], str[1], str[2], str[3]));

				}

			}


		}

	}
}
