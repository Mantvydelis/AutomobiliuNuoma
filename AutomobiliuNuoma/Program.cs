﻿using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using AutomobiliuNuoma.Core.Repositories;
using AutomobiliuNuoma.Core.Services;
using System;
using System.Linq;

namespace AutomobiliuNuomaConsoleApp;

public class Program
{
    public static void Main(string[] args)
    {
        IAutonuomaService autonuomaService = SetupDependencies();
        while(true)
        {
            Console.WriteLine("1. Rodyti visus automobilius (KOL KAS NEVEIKIA)");
            Console.WriteLine("2. Rodyti visus klientus");
            Console.WriteLine("3. Rodyti visus elektromobilius is duombazes");
            Console.WriteLine("4. Rodyti visus naftos kuro automobilius is duombazes");
            Console.WriteLine("5. Prideti automobili i duombaze");
            Console.WriteLine("6. Rodyti visus klientus is duombazes");
            Console.WriteLine("7. Prideti klienta i duombaze");
            Console.WriteLine("8. Gauti visus uzsakymus is duombazes");
            Console.WriteLine("9. Formuoti nuomos uzsakyma i duombaze");
            Console.WriteLine("10. Pakeisti duomenis automobiliu duombazeje");
            Console.WriteLine("11. Pakeisti duomenis klientu duombazeje");
            Console.WriteLine("12. Pakeisti duomenis uzsakymu duombazeje");

            string pasirinkimas = Console.ReadLine();
            switch (pasirinkimas)
            {
                case "1": /*neveikia*/
                    List<Automobilis> auto = autonuomaService.GautiVisusAutomobilius();
                    foreach (Automobilis a in auto)
                    {
                        Console.WriteLine(a);
                    }
                    break;
                case "2": /*PERMETA I DUOMBAZE, O NE LISTA*/
                    List<Klientas> klientai = autonuomaService.GautiVisusKlientus();
                    foreach (Klientas k in klientai)
                    {
                        Console.WriteLine(k);
                    }
                    break;
                case "3": /*duombaze*/
                    List<Elektromobilis> elektromobiliai = autonuomaService.GautiVisusElektromobilius();
                    foreach (Elektromobilis ev in elektromobiliai)
                    {
                        Console.WriteLine(ev);
                    }
                    break;
                case "4": /*duombaze*/
                    List<NaftosKuroAutomobilis> naftosKuroAutomobiliai = autonuomaService.GautiVisusNaftosKuroAuto();
                    foreach (NaftosKuroAutomobilis v in naftosKuroAutomobiliai)
                    {
                        Console.WriteLine(v);
                    }
                    break;

                case "5": /*duombaze*/
                    Automobilis naujasAuto = new Automobilis();
                    int ikrovimoLaikas = 0;
                    int baterijosTalpa = 0;
                    double kuroSanaudos = 0;
                    Console.WriteLine("Elektromobilis - 1  Naftos Kuro Auto - 2: ");
                    string tipas = Console.ReadLine();
                    switch (tipas)
                    {
                        case "1":
                            Console.WriteLine("Iveskite Ikrovimo laika");
                            ikrovimoLaikas = int.Parse(Console.ReadLine());
                            Console.WriteLine("Iveskite Baterijos talpa");
                            baterijosTalpa = int.Parse(Console.ReadLine());
                            break;
                        case "2":
                            Console.WriteLine("Iveskite kuro sanaudas");
                            kuroSanaudos = double.Parse(Console.ReadLine());
                            break;
                    }
                    Console.WriteLine("Iveskite marke");
                    string marke = Console.ReadLine();
                    Console.WriteLine("Iveskite modeli");
                    string modelis = Console.ReadLine();
                    Console.WriteLine("Iveskite nuomos kaina");
                    decimal nuomosKaina = decimal.Parse(Console.ReadLine());
                    switch (tipas)
                    {
                        case "1":
                            naujasAuto = new Elektromobilis(marke, modelis, nuomosKaina, baterijosTalpa, ikrovimoLaikas);
                            break;
                        case "2":
                            naujasAuto = new NaftosKuroAutomobilis(marke, modelis, nuomosKaina, kuroSanaudos);
                            break;
                    }
                    autonuomaService.PridetiNaujaAutomobili(naujasAuto);

                    break;

                case "6": /*duombaze*/
                    List<Klientas> klientaiDB = autonuomaService.GautiVisusKlientus();
                    foreach (Klientas kl in klientaiDB)
                    {
                        Console.WriteLine(kl);
                    }
                    break;

                case "7":
                    
                    Console.WriteLine("Iveskite kliento varda");
                    string klientoVardas = Console.ReadLine();
                    Console.WriteLine("Iveskite kliento pavarde");
                    string klientoPavarde = Console.ReadLine();
                    Console.WriteLine("Iveskite kliento gimimo data (yyyy-mm-dd)");
                    DateOnly klientoGimimoData = DateOnly.Parse(Console.ReadLine());

                    Klientas naujasKlientas = new Klientas(klientoVardas, klientoPavarde, klientoGimimoData);

                    autonuomaService.PridetiNaujaKlienta(naujasKlientas);

                    break;

                case "8": /*gauti uzsakymus is duombazes*/
                    var uzsakymai = autonuomaService.GautiVisusUzsakymus();
                    if (uzsakymai.Count == 0)
                    {
                        Console.WriteLine("Nera uzsakymu.");
                    }
                    else
                    { 
                        var naftosKuroAutomobiliai2 = autonuomaService.GautiVisusNaftosKuroAuto();
                        var elektromobiliai2 = autonuomaService.GautiVisusElektromobilius();
                        var klientai2 = autonuomaService.GautiVisusKlientus();

                        foreach (var uzsakymas in uzsakymai)
                        {
                            var klientas = klientai2.FirstOrDefault(k => k.KlientasId == uzsakymas.KlientasId);

                            Automobilis automobilis = null;

                            if (uzsakymas.AutoTipas == "NaftosKuroAutomobilis")
                            {
                                automobilis = naftosKuroAutomobiliai2
                                    .FirstOrDefault(a => a.AutomobilisId == uzsakymas.AutomobilisId);
                            }
                            else if (uzsakymas.AutoTipas == "Elektromobilis")
                            {
                                automobilis = elektromobiliai2
                                    .FirstOrDefault(a => a.AutomobilisId == uzsakymas.AutomobilisId);
                            }

                            if (automobilis != null)
                            {
                                Console.WriteLine($"Uzsakovas: {klientas.Vardas} {klientas.Pavarde}, Uzsakytas automobilis: {automobilis.Marke} {automobilis.Modelis}, Nuomos Pradzia: {uzsakymas.NuomosPradzia.ToShortDateString()}, Dienu Kiekis: {uzsakymas.DienuKiekis} Pabaigos Data: {uzsakymas.gautiPabaigosData().ToShortDateString()}");
                            }
                        }
                    }
                    break;

                case "9": /*Prideti prie duombazes uzsakyma*/

                    Console.WriteLine("Pasirinkite kliento id:");
                    var visiKlientai = autonuomaService.GautiVisusKlientus();
                    foreach (var k in visiKlientai) Console.WriteLine($"{k.KlientasId} {k.Vardas} {k.Pavarde}");
                    int klientasId = int.Parse(Console.ReadLine());

                    Console.WriteLine("Iveskite nuomos pradzios data (yyyy-mm-dd):");
                    DateTime nuomosPradzia = DateTime.Parse(Console.ReadLine());

                    Console.WriteLine("Iveskite dienu kieki:");
                    int nuomosDienuKiekis = int.Parse(Console.ReadLine());

                    Console.WriteLine("Elektromobilis - 1  Naftos Kuro Auto - 2: ");
                    string AutoTipas = Console.ReadLine();

                    switch (AutoTipas)
                    {
                        case "1":
                            Console.WriteLine("Pasirinkite automobilio id:");
                            var visiAuto1 = autonuomaService.GautiVisusElektromobilius();
                            foreach (var a in visiAuto1) Console.WriteLine($"{a.AutomobilisId} {a.Marke} {a.Modelis}");
                            int automobilisId1 = int.Parse(Console.ReadLine());
                            autonuomaService.SukurtiNuoma(klientasId, automobilisId1, nuomosPradzia, nuomosDienuKiekis, AutoTipas);
                            break;
                        case "2":
                            Console.WriteLine("Pasirinkite automobilio id:");
                            var visiAuto2 = autonuomaService.GautiVisusNaftosKuroAuto();
                            foreach (var a in visiAuto2) Console.WriteLine($"{a.AutomobilisId} {a.Marke} {a.Modelis}");
                            int automobilisId2 = int.Parse(Console.ReadLine());
                            autonuomaService.SukurtiNuoma(klientasId, automobilisId2, nuomosPradzia, nuomosDienuKiekis, AutoTipas);
                            break;
                    }
                    break;

                case "10": //Pakeisti duomenis automobiliu duombazeje
                    Console.WriteLine("Pasirinkite automobiliu sarasa: 1 - Naftos kuro automobiliai, 2 - Elektromobiliai:");
                    string keitimoTipas = Console.ReadLine();
                    switch (keitimoTipas)
                    {
                        case "1":

                            Console.WriteLine("Pasirinkite norimo keisti automobilio id:");
                            int id = int.Parse(Console.ReadLine());
                            var dabartineAuto = autonuomaService.GautiNaftosAutoPagalId(id);

                            Console.WriteLine("Iveskite nauja marke arba spauskite ENTER:");
                            string naujaMarke = Console.ReadLine();
                            if (naujaMarke == null)
                            {
                                naujaMarke = dabartineAuto.Marke;
                            }
                            Console.WriteLine("Iveskite nauja modeli arba spauskite ENTER:");
                            string naujasModelis = Console.ReadLine();
                            if (naujasModelis == null)
                            {
                                naujasModelis = dabartineAuto.Modelis;
                            }
                            Console.WriteLine("Iveskite nauja nuomos kaina arba spauskite ENTER:");
                            decimal naujaNuomosKaina = decimal.Parse(Console.ReadLine());
                            if (naujaNuomosKaina == null)
                            {
                                naujaNuomosKaina = dabartineAuto.NuomosKaina;
                            }
                            Console.WriteLine("Iveskite naujas degalu sanaudas arba spauskite ENTER:");
                            double naujosDegaluSanaudos = double.Parse(Console.ReadLine());
                            if (naujosDegaluSanaudos == null)
                            {
                                naujosDegaluSanaudos = dabartineAuto.DegaluSanaudos;
                            }

                            var atnaujintasAutomobilis = autonuomaService.KoreguotiNaftaAutoInfo(id, naujaMarke, naujasModelis, naujaNuomosKaina, naujosDegaluSanaudos);

                            break;


                        case "2":
                            
                        Console.WriteLine("Pasirinkite norimo keisti elektromobilio id:");
                        var visiAuto1 = autonuomaService.GautiVisusElektromobilius();
                        foreach (var a in visiAuto1) Console.WriteLine($"{a.AutomobilisId} {a.Marke} {a.Modelis} {a.NuomosKaina} {a.BaterijosTalpa}, {a.KrovimoLaikas}");

                            break;


                    }

                 break;


                 default:
                 Console.WriteLine("Neteisingas pasirinkimas. Bandykite dar karta.");
                 break;

            }


        }
    }
    public static IAutonuomaService SetupDependencies()
    {
        //IKlientaiRepository klientaiRepository = new KlientaiFileRepository("Klientai.csv");
        //IAutomobiliaiRepository automobiliaiRepository = new AutomobiliaiFileRepository("Auto.csv");
        IKlientaiRepository klientaiRepository = new KlientaiDBRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;");
        IAutomobiliaiRepository automobiliaiRepository = new AutomobiliaiDbRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;");
        IUzsakymaiRepository uzsakymaiRepository = new UzsakymaiDBRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;");

        IKlientaiService klientaiService = new KlientaiService(klientaiRepository);
        IAutomobiliaiService automobiliaiService = new AutomobiliaiService(automobiliaiRepository);
        return new AutonuomosService(klientaiService, automobiliaiService, uzsakymaiRepository);
    }
}