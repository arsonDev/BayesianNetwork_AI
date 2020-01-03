using Encog.ML.Bayesian;
using Encog.ML.Bayesian.Query.Enumeration;
using System;

namespace AI_BayesianNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            // siec
            BayesianNetwork network = new BayesianNetwork();
            // zdarzenie Ubera
            BayesianEvent uberDriver = network.CreateEvent("kierowca_ubera");
            // zdarzenie swiatka
            BayesianEvent witnessSawUberDriver = network.CreateEvent("widział kierowce ubera");

            //polaczenie obu zdarzen
            network.CreateDependency(uberDriver, witnessSawUberDriver);
            network.FinalizeStructure();

            // tworzenie tablicy prawdy
            // dodanie prawdopodobienstwa zdarzen
            uberDriver.Table.AddLine(0.85, true);
            witnessSawUberDriver.Table.AddLine(0.8, true, true);
            witnessSawUberDriver.Table.AddLine(0.2, true, false);

            network.Validate();

            Console.WriteLine(network.ToString());
            Console.WriteLine($"Liczba parametrów: {network.CalculateParameterCount()}");

            EnumerationQuery query = new EnumerationQuery(network);

            query.DefineEventType(witnessSawUberDriver, EventType.Evidence);
            query.DefineEventType(uberDriver, EventType.Outcome);

            query.SetEventValue(witnessSawUberDriver, false);
            query.SetEventValue(uberDriver, false);

            query.Execute();
            Console.WriteLine(query.ToString());
        }
    }
}
