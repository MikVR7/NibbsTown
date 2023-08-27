using System.Collections.Generic;
using static NibbsTown.Description;

namespace NibbsTown
{
    internal class RallyCreator
    {
        internal enum NewRallyType
        {
            None = 0,
            RallyGrazMur = 1,
            RallyGrazSchlossberg = 2,
            RallyOedWald = 3,
        }

        internal static EventIn_AddNewRallyToServer EventIn_AddNewRallyToServer = new EventIn_AddNewRallyToServer();

        internal void Init()
        {
            EventIn_AddNewRallyToServer.AddListenerSingle(AddNewRallyToServer);
        }

        private void AddNewRallyToServer(NewRallyType rallyType)
        {
            if (rallyType == NewRallyType.RallyGrazMur) { this.CreateRallyGrazMur(); }
            if (rallyType == NewRallyType.RallyGrazSchlossberg) { this.CreateRallyGrazSchlossberg(); }
            if (rallyType == NewRallyType.RallyOedWald) { this.CreateRallyOedWald(); }
        }

        private void CreateRallyGrazMur()
        {
            Rally rally = CreateRally("MurRally", "Entdecke die Mur in Graz");

            // description
            List<Description> descriptions = new List<Description>();
            descriptions.Add(new Description { Type = DescriptionType.Image, Data = "descr_00.jpg" });
            descriptions.Add(new Description { Type = DescriptionType.Text, Data = "In der Stadt und auf der Höh’ – das schließt sich in Graz keineswegs aus. Der Fluss, die einzigartige Altstadt und mitten drin ein Berg. Der Grazer Schlossberg ist Sehenswürdigkeit, Naturschauspiel, Naherholungsgebiet und Aussichtspunkt zugleich. In kürzester Zeit gelangt man nach oben und genießt den herrlichen Ausblick auf Graz, die Altstadt und Umgebung. Einer Burg, die vor über 1.000 Jahren auf dem Schlossberg errichtet wurde, verdankt die Stadt ihren Namen. Aus dem slawischen Gradec für ,kleine Burg‘ wurde später Graz.\r\n\r\nSeit 1894 überwindet die Schlossbergbahn bravourös die rund 60% Steigung hinauf zum Schlossbergplateau - mit modernen Panoramagondeln.\r\n\r\nEine schnellere Art den Schlossberg zu erreichen, ist die Fahrt mit dem Schlossberglift." });
            descriptions.Add(new Description { Type = DescriptionType.Image, Data = "descr_00.jpg" });
            descriptions.Add(new Description { Type = DescriptionType.Text, Data = "In der Stadt und auf der Höh’ – das schließt sich in Graz keineswegs aus. Der Fluss, die einzigartige Altstadt und mitten drin ein Berg. Der Grazer Schlossberg ist Sehenswürdigkeit, Naturschauspiel, Naherholungsgebiet und Aussichtspunkt zugleich. In kürzester Zeit gelangt man nach oben und genießt den herrlichen Ausblick auf Graz, die Altstadt und Umgebung. Einer Burg, die vor über 1.000 Jahren auf dem Schlossberg errichtet wurde, verdankt die Stadt ihren Namen. Aus dem slawischen Gradec für ,kleine Burg‘ wurde später Graz.\r\n\r\nSeit 1894 überwindet die Schlossbergbahn bravourös die rund 60% Steigung hinauf zum Schlossbergplateau - mit modernen Panoramagondeln.\r\n\r\nEine schnellere Art den Schlossberg zu erreichen, ist die Fahrt mit dem Schlossberglift." });
            rally.Descr = descriptions.ToArray();

            // station0
            Station station0 = this.CreateStation(0, "Mur1", 15.321f, 41.123f);
            // tasks
            RallyTask task0_0 = this.CreateTask(0, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Hop hop, auf zum Mur!" } });
            RallyTask task0_1 = this.CreateTask(1, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Hop hop, auf zum Mur! Zum zweiten Mal!" } });
            station0.Tasks = new List<RallyTask>() { task0_0, task0_1 }.ToArray();

            // station1
            Station station1 = this.CreateStation(1, "Mur2", 15.321f, 41.223f);
            // tasks
            RallyTask task1_0 = this.CreateTask(0, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Hop hop, auf zum Mur, Station 2!" } });
            RallyTask task1_1 = this.CreateTask(1, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Hop hop, auf zum Mur, Station 2! Zum zweiten Mal!" } });
            station1.Tasks = new List<RallyTask>() { task1_0, task1_1 }.ToArray();

            // endscreen
            List<Description> endscreen = new List<Description>();
            endscreen.Add(new Description { Type = DescriptionType.Text, Data = "Gratuliere!<br><br>Du hast die Murrally erfolgreich abgeschlossen!" });
            rally.Endscreen = endscreen.ToArray();

            rally.Stations = new List<Station>() { station0, station1 }.ToArray();
            rally.CenterPos = MapsTools.CalculateCenterPosition(rally.Stations);
            rally.Zoom = MapsTools.GetZoomLevel(rally.Stations);

            DatabaseRallies.EventIn_AddRallyToDB.Invoke(rally);
        }

        private void CreateRallyGrazSchlossberg()
        {
            Rally rally = CreateRally("Schloßberg Rally", "Ausblick inmitten der Altstadt");

            // description
            List<Description> descriptions = new List<Description>();
            descriptions.Add(new Description { Type = DescriptionType.Image, Data = "descr_00.jpg" });
            descriptions.Add(new Description { Type = DescriptionType.Text, Data = "Der Schloßberg in Graz bildet mit 123 Metern Höhe, ausgehend vom Grazer Hauptplatz, den höchsten natürlichen Punkt der Stadt und bietet einen 360° Rundblick über die Stadt Graz und deren Grenzen hinaus. Beginnen wir mit der Geschichte des Schloßbergs. Im 12. Jahrhundert wurde auf dem Schloßberg eine Burg errichtet, die der Stadt Graz auch ihren Namen gab. Einer Ableitung aus „gradec“ – dem slowenischen Begriff für kleine Burg. Da die Burg nie erobert wurde, ist sie im Guinness Buch der Rekorde als die stärkste Festung aller Zeiten aufgelistet. Nicht einmal Napoleon schaffte es im 19. Jahrhundert die Burg einzunehmen. Erst als er durch die Besetzung Wiens 1809 Graz erpresste, Wien zu zerstören, ergab sich die Stadt Graz. Bis auf den Glockenturm und den Uhrturm, die von den Grazern freigekauft wurden, wurde die Burg im Großen und Ganzen abgetragen und gesprengt, eine sogenannte Schleifung. 30 Jahre später legte Ludwig Freiherr von Weldenman Spazierwege und einen romantischen Garten am Schloßberg an." });
            rally.Descr = descriptions.ToArray();

            // station0
            Station station0 = this.CreateStation(0, "Sb0", 15.440524f, 47.073826f);
            // tasks
            RallyTask task0_0 = this.CreateTask(0, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Hallo und herzlich willkommen bei der<br><b><size=130%>Schloßberg-Townrallye!" } });
            //RallyTask task0_1 = this.CreateTask(1, RallyTask.Type.Game_Objectfind, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Um die Rallye zu starten, suche mit deinem Handy eine Teufelsfigur am Kamerliterplatz!" } });
            station0.Tasks = new List<RallyTask>() { task0_0/*, task0_1 */}.ToArray();

            // station1
            Station station1 = this.CreateStation(1, "Sb1", 15.437642f, 47.073671f);
            // tasks
            RallyTask task1_0 = this.CreateTask(0, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Hallo und herzlich willkommen bei der<br><b><size=130%>Schloßberg-Townrallye!" } });
            station1.Tasks = new List<RallyTask>() { task1_0 }.ToArray();

            // endscreen
            List<Description> endscreen = new List<Description>();
            endscreen.Add(new Description { Type = DescriptionType.Text, Data = "Gratuliere!<br><br>Du hast die Schloßbergrally erfolgreich abgeschlossen!" });
            rally.Endscreen = endscreen.ToArray();

            rally.Stations = new List<Station>() { station0, station1 }.ToArray();
            rally.CenterPos = MapsTools.CalculateCenterPosition(rally.Stations);
            rally.Zoom = MapsTools.GetZoomLevel(rally.Stations);

            DatabaseRallies.EventIn_AddRallyToDB.Invoke(rally);
        }

        private void CreateRallyOedWald()
        {
            Rally rally = CreateRally("Zauberwald", "Willkommen im Zauberwald");

            // description
            List<Description> descriptions = new List<Description>();
            descriptions.Add(new Description { Type = DescriptionType.Image, Data = "descr_00.jpg" });
            descriptions.Add(new Description { Type = DescriptionType.Text, Data = "Beschreibung des Waldes bitte hier einfügen..." });
            descriptions.Add(new Description { Type = DescriptionType.Image, Data = "descr_01.jpg" });
            rally.Descr = descriptions.ToArray();

            // station0
            Station station0 = this.CreateStation(0, "Start", 15.868403f, 47.057178f);
            // tasks
            RallyTask task0_0 = this.CreateTask(0, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            RallyTask task0_1 = this.CreateTask(1, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            RallyTask task0_2 = this.CreateTask(2, RallyTask.Type.Task_Cloze, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Fülle die Lücken sinnvoll und korrekt aus.\n\nDie Geschichte des Uhrturms liegt lange zurück, denn bereits <cl-dd>1265</cl-dd> finden sich erste Aufzeichnungen des Turms als Teil der Festungsanlage am Schloßberg, und seit 1560 steht er in seiner heutigen Form an diesem Platz verankert. Damals diente er noch als <cl-dd>Feuerwache</cl-dd>, denn die Feuerwächter konnten Brände und Feuer in der Stadt erkennen und die <cl-dd>Feuerglocke</cl-dd> läuten. Die <cl-dd>Anzahl</cl-dd> der Schläge der Feuerglocke stand für jeweils einen anderen Grazer Bezirk in dem es brannte. Als die <cl-dd>Franzosen</cl-dd> Graz belagerten, begannen sie damit die Grazer Festungsanlage zu <cl-in>schleifen</cl-in>. Dem Uhrturm blieb dieses Schicksal erspart, denn er wurde von Grazer Bürgern <cl-in>freigekauft</cl-in>." } });
            RallyTask task0_3 = this.CreateTask(3, RallyTask.Type.Task_Cloze, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Das ist der Zweite Lückentext:\n\n Die <cl-dd>Anzahl</cl-dd> der Schläge der Feuerglocke stand für jeweils einen anderen Grazer Bezirk in dem es brannte. Als die <cl-dd>Franzosen</cl-dd> Graz belagerten, begannen sie damit die Grazer Festungsanlage zu <cl-in>schleifen</cl-in>. Dem Uhrturm blieb dieses Schicksal erspart, denn er wurde von Grazer Bürgern <cl-in>freigekauft</cl-in>." } });
            RallyTask task0_4 = this.CreateTask(4, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            RallyTask task0_5 = this.CreateTask(5, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            station0.Tasks = new List<RallyTask>() { task0_0, task0_1, task0_2, task0_3, task0_4, task0_5 }.ToArray();

            // station1
            Station station1 = this.CreateStation(1, "Wald 1", 15.866783f, 47.061213f);
            //// tasks
            RallyTask task1_0 = this.CreateTask(0, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            RallyTask task1_1 = this.CreateTask(1, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            RallyTask task1_2 = this.CreateTask(2, RallyTask.Type.Task_Cloze, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Fülle die Lücken sinnvoll und korrekt aus.\n\nDie Geschichte des Uhrturms liegt lange zurück, denn bereits <cl-dd>1265</cl-dd> finden sich erste Aufzeichnungen des Turms als Teil der Festungsanlage am Schloßberg, und seit 1560 steht er in seiner heutigen Form an diesem Platz verankert. Damals diente er noch als <cl-dd>Feuerwache</cl-dd>, denn die Feuerwächter konnten Brände und Feuer in der Stadt erkennen und die <cl-dd>Feuerglocke</cl-dd> läuten. Die <cl-dd>Anzahl</cl-dd> der Schläge der Feuerglocke stand für jeweils einen anderen Grazer Bezirk in dem es brannte. Als die <cl-dd>Franzosen</cl-dd> Graz belagerten, begannen sie damit die Grazer Festungsanlage zu <cl-in>schleifen</cl-in>. Dem Uhrturm blieb dieses Schicksal erspart, denn er wurde von Grazer Bürgern <cl-in>freigekauft</cl-in>." } });
            RallyTask task1_3 = this.CreateTask(3, RallyTask.Type.Task_Cloze, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Das ist der Zweite Lückentext:\n\n Die <cl-dd>Anzahl</cl-dd> der Schläge der Feuerglocke stand für jeweils einen anderen Grazer Bezirk in dem es brannte. Als die <cl-dd>Franzosen</cl-dd> Graz belagerten, begannen sie damit die Grazer Festungsanlage zu <cl-in>schleifen</cl-in>. Dem Uhrturm blieb dieses Schicksal erspart, denn er wurde von Grazer Bürgern <cl-in>freigekauft</cl-in>." } });
            RallyTask task1_4 = this.CreateTask(4, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            RallyTask task1_5 = this.CreateTask(5, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });

            station1.Tasks = new List<RallyTask>() { task1_0, task1_1, task1_2, task1_3, task1_4, task1_5 }.ToArray();

            // station2
            Station station2 = this.CreateStation(2, "Wald 2", 15.868046f, 47.060489f);
            // tasks
            RallyTask task2_0 = this.CreateTask(0, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            RallyTask task2_1 = this.CreateTask(1, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            RallyTask task2_2 = this.CreateTask(2, RallyTask.Type.Task_Cloze, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Fülle die Lücken sinnvoll und korrekt aus.\n\nDie Geschichte des Uhrturms liegt lange zurück, denn bereits <cl-dd>1265</cl-dd> finden sich erste Aufzeichnungen des Turms als Teil der Festungsanlage am Schloßberg, und seit 1560 steht er in seiner heutigen Form an diesem Platz verankert. Damals diente er noch als <cl-dd>Feuerwache</cl-dd>, denn die Feuerwächter konnten Brände und Feuer in der Stadt erkennen und die <cl-dd>Feuerglocke</cl-dd> läuten. Die <cl-dd>Anzahl</cl-dd> der Schläge der Feuerglocke stand für jeweils einen anderen Grazer Bezirk in dem es brannte. Als die <cl-dd>Franzosen</cl-dd> Graz belagerten, begannen sie damit die Grazer Festungsanlage zu <cl-in>schleifen</cl-in>. Dem Uhrturm blieb dieses Schicksal erspart, denn er wurde von Grazer Bürgern <cl-in>freigekauft</cl-in>." } });
            RallyTask task2_3 = this.CreateTask(3, RallyTask.Type.Task_Cloze, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Das ist der Zweite Lückentext:\n\n Die <cl-dd>Anzahl</cl-dd> der Schläge der Feuerglocke stand für jeweils einen anderen Grazer Bezirk in dem es brannte. Als die <cl-dd>Franzosen</cl-dd> Graz belagerten, begannen sie damit die Grazer Festungsanlage zu <cl-in>schleifen</cl-in>. Dem Uhrturm blieb dieses Schicksal erspart, denn er wurde von Grazer Bürgern <cl-in>freigekauft</cl-in>." } });
            RallyTask task2_4 = this.CreateTask(4, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            RallyTask task2_5 = this.CreateTask(5, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            station2.Tasks = new List<RallyTask>() { task2_0, task2_1, task2_2, task2_3, task2_4, task2_5 }.ToArray();

            // station3
            Station station3 = this.CreateStation(2, "Hexenhaus", 15.871939f, 47.057089f);
            // tasks
            RallyTask task3_0 = this.CreateTask(0, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            RallyTask task3_1 = this.CreateTask(1, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            RallyTask task3_2 = this.CreateTask(2, RallyTask.Type.Task_Cloze, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Fülle die Lücken sinnvoll und korrekt aus.\n\nDie Geschichte des Uhrturms liegt lange zurück, denn bereits <cl-dd>1265</cl-dd> finden sich erste Aufzeichnungen des Turms als Teil der Festungsanlage am Schloßberg, und seit 1560 steht er in seiner heutigen Form an diesem Platz verankert. Damals diente er noch als <cl-dd>Feuerwache</cl-dd>, denn die Feuerwächter konnten Brände und Feuer in der Stadt erkennen und die <cl-dd>Feuerglocke</cl-dd> läuten. Die <cl-dd>Anzahl</cl-dd> der Schläge der Feuerglocke stand für jeweils einen anderen Grazer Bezirk in dem es brannte. Als die <cl-dd>Franzosen</cl-dd> Graz belagerten, begannen sie damit die Grazer Festungsanlage zu <cl-in>schleifen</cl-in>. Dem Uhrturm blieb dieses Schicksal erspart, denn er wurde von Grazer Bürgern <cl-in>freigekauft</cl-in>." } });
            RallyTask task3_3 = this.CreateTask(3, RallyTask.Type.Task_Cloze, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Das ist der Zweite Lückentext:\n\n Die <cl-dd>Anzahl</cl-dd> der Schläge der Feuerglocke stand für jeweils einen anderen Grazer Bezirk in dem es brannte. Als die <cl-dd>Franzosen</cl-dd> Graz belagerten, begannen sie damit die Grazer Festungsanlage zu <cl-in>schleifen</cl-in>. Dem Uhrturm blieb dieses Schicksal erspart, denn er wurde von Grazer Bürgern <cl-in>freigekauft</cl-in>." } });
            RallyTask task3_4 = this.CreateTask(4, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            RallyTask task3_5 = this.CreateTask(5, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim ersten Waldstück!" } });
            station3.Tasks = new List<RallyTask>() { task3_0, task3_1, task3_2, task3_3, task3_4, task3_5 }.ToArray();

            //// station4
            //Station station4 = this.CreateStation(4, "Chickenhome", 15.871932f, 47.060899f);
            //// tasks
            //RallyTask task4_0 = this.CreateTask(0, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Die Geister tanzen hier!" } });
            //station4.Tasks = new List<RallyTask>() { task4_0 }.ToArray();

            //// station5
            //Station station5 = this.CreateStation(5, "Valley", 15.871713f, 47.051357f);
            //// tasks
            //RallyTask task5_0 = this.CreateTask(0, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Die Waldlichtung erleuchtet dir den Weg..." } });
            //station5.Tasks = new List<RallyTask>() { task5_0 }.ToArray();

            //// station6
            //Station station6 = this.CreateStation(6, "Settlement", 15.875369f, 47.052431f);
            //// tasks
            //RallyTask task6_0 = this.CreateTask(0, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Herzlichen Glückwunsch! Du hast die Oeder Waldrunde gut überstanden." } });
            //station6.Tasks = new List<RallyTask>() { task6_0 }.ToArray();

            // endscreen
            List<Description> endscreen = new List<Description>();
            endscreen.Add(new Description { Type = DescriptionType.Text, Data = "Gratuliere!<br><br>Du hast die Rally 'Zauberwald' erfolgreich abgeschlossen!" });
            rally.Endscreen = endscreen.ToArray();

            rally.Stations = new List<Station>() { station0, station1, station2, station3/*, station4, station5, station6 */}.ToArray();
            rally.CenterPos = MapsTools.CalculateCenterPosition(rally.Stations);
            rally.Zoom = MapsTools.GetZoomLevel(rally.Stations);

            DatabaseRallies.EventIn_AddRallyToDB.Invoke(rally);
        }

        private Rally CreateRally(string rallyName, string caption)
        {
            Rally rally = new Rally();
            rally.Name = rallyName;
            rally.Caption = caption;
            return rally;
        }
        
        private Station CreateStation(int id, string name, float posX, float posY)
        {
            Station station = new Station();
            station.Index = id;
            station.Pos = new GPSPosition(posX, posY);
            station.Name = name;
            return station;
        }

        private RallyTask CreateTask(int index, RallyTask.Type taskType, Description[] descr)
        {
            RallyTask task = new RallyTask();
            task.Id = index;
            task.Descr = descr;
            task.TType = taskType;
            return task;
        }
    }
}
