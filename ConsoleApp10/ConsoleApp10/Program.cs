//Erik Daniel Otalora Alba
//El PIN es 1234


using System;

class Cliente
{
    public string Nombre { get; }
    public decimal Saldo { get; private set; }
    public decimal TopeDiarioRetiro { get; }
    public decimal PuntosViveColombia { get; private set; }

    private static decimal cantidadRetiradaHoy = 0;

    public Cliente(string nombre, decimal saldo, decimal topeDiarioRetiro)
    {
        Nombre = nombre;
        Saldo = saldo;
        TopeDiarioRetiro = topeDiarioRetiro;
        PuntosViveColombia = 0;
    }

    public bool Autenticar(string pin)
    {
        if (pin == "1234")
        {
            return true;
        }
        else
        {
            throw new Exception("Autenticación fallida. PIN incorrecto.");
        }
    }

    public decimal ConsultarSaldo()
    {
        return Saldo;
    }

    public string RealizarRetiro(decimal cantidad)
    {
        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad de retiro debe ser mayor a cero.");
        }

        if (cantidad > Saldo)
        {
            throw new ArgumentException("Saldo insuficiente para realizar el retiro.");
        }

        if (cantidad > TopeDiarioRetiro - cantidadRetiradaHoy)
        {
            throw new ArgumentException("El retiro supera el tope diario de retiros restante.");
        }

        Saldo -= cantidad;
        cantidadRetiradaHoy += cantidad;
        return $"Retiro exitoso. Nuevo saldo: {Saldo}";
    }

    public void RealizarTransferencia(Cliente cuentaDestino, decimal cantidad)
    {
        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad de transferencia debe ser mayor a cero.");
        }

        if (cantidad > Saldo)
        {
            throw new ArgumentException("Saldo insuficiente para realizar la transferencia.");
        }

        Saldo -= cantidad;
        cuentaDestino.Saldo += cantidad;

        Console.WriteLine($"Transferencia exitosa. Nuevo saldo en {Nombre}: {Saldo}");
        Console.WriteLine($"Nuevo saldo en cuenta destino ({cuentaDestino.Nombre}): {cuentaDestino.Saldo}");
    }

    public void ConsultarPuntosViveColombia()
    {
        Console.WriteLine($"Puntos ViveColombia disponibles: {PuntosViveColombia}");
    }

    public void CanjearPuntosViveColombia(decimal puntos)
    {
        if (puntos <= 0)
        {
            throw new ArgumentException("La cantidad de puntos a canjear debe ser mayor a cero.");
        }

        if (PuntosViveColombia < puntos)
        {
            throw new ArgumentException("Puntos insuficientes para realizar el canje.");
        }

        decimal valorCanje = puntos * 7;
        Saldo += valorCanje;
        PuntosViveColombia -= puntos;
        Console.WriteLine($"Canje exitoso. Nuevo saldo: {Saldo}");
    }
}

class Program
{
    static decimal cantidadRetiradaHoy = 0;

    static void Main()
    {
        Cliente cliente1 = new Cliente("Juan", 5000000, 2000000);
        Cliente cuentaDestino = new Cliente("Destino", 0, 0);

        try
        {
            Console.Write("Ingrese su PIN: ");
            string pinIngresado = Console.ReadLine();
            cliente1.Autenticar(pinIngresado);

            while (true)
            {
                Console.WriteLine("\nMenú de Operaciones:");
                Console.WriteLine("1. Consultar Saldo");
                Console.WriteLine("2. Realizar Retiro");
                Console.WriteLine("3. Realizar Transferencia");
                Console.WriteLine("4. Consultar Puntos ViveColombia");
                Console.WriteLine("5. Canjear Puntos ViveColombia");
                Console.WriteLine("6. Salir");

                Console.Write("Seleccione una opción (1-6): ");
                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine($"Saldo actual: {cliente1.ConsultarSaldo()}");
                        break;

                    case 2:
                        Console.Write("Ingrese la cantidad a retirar: ");
                        decimal cantidadRetiro = decimal.Parse(Console.ReadLine());
                        string resultadoRetiro = cliente1.RealizarRetiro(cantidadRetiro);
                        Console.WriteLine(resultadoRetiro);
                        break;

                    case 3:
                        Console.Write("Ingrese la cantidad a transferir: ");
                        decimal cantidadTransferencia = decimal.Parse(Console.ReadLine());
                        cliente1.RealizarTransferencia(cuentaDestino, cantidadTransferencia);
                        break;

                    case 4:
                        cliente1.ConsultarPuntosViveColombia();
                        break;

                    case 5:
                        Console.Write("Ingrese la cantidad de puntos a canjear: ");
                        decimal puntosCanje = decimal.Parse(Console.ReadLine());
                        cliente1.CanjearPuntosViveColombia(puntosCanje);
                        break;

                    case 6:
                        Console.WriteLine("Saliendo del programa. Gracias por utilizar el cajero automático.");
                        return;

                    default:
                        Console.WriteLine("Opción no válida. Por favor, seleccione una opción del 1 al 6.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}