using System;

namespace SmartPark
{
	class Program
	{
		static void Main(string[] args)
		{

			// DECLARACIÓN DE VARIABLES

			// Variables del Sistema
			string nombreOperador = "";
			string codigoTurno = "";
			// Capacidad fijada a 10 según instrucción
			int capacidadTotal = 10;
			int ticketsCreados = 0;
			int ticketsCerrados = 0;
			double dineroRecaudado = 0.0;
			int tiempoSimulado = 0; // En minutos

			// Variables del Ticket Activo
			bool ticketActivo = false;
			string placa = "";
			int tipoVehiculo = 0; // 1=Moto, 2=Auto, 3=Pickup
			string nombreCliente = "";
			bool esVip = false;
			int minutoEntrada = 0;

			// Variables Auxiliares
			string opcionMenu = "";
			int minutosIngresados = 0;
			int minutosEstacionados = 0;
			double tarifaPorHora = 0;
			double horasCobradas = 0;
			double cobroBase = 0;
			double multa = 0;
			double descuento = 0;
			double recargoPermanencia = 0;
			double montoFinal = 0;
			bool entradaValida = false;

			// ==========================================
			// 1. REGISTRO INICIAL DEL SISTEMA
			// ==========================================
			Console.Title = "SmartPark - Sistema de Parqueo Inteligente";

			// Color Azul reemplazando a Cyan
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("================================================================");
			Console.WriteLine("   _____ __  __          _____ _______ _____         _____  _  __");
			Console.WriteLine("  / ____|  \\/  |   /\\   |  __ \\__   __|  __ \\   /\\  |  __ \\| |/ /");
			Console.WriteLine(" | (___ | \\  / |  /  \\  | |__) | | |  | |__) | /  \\ | |__) | ' / ");
			Console.WriteLine("  \\___ \\| |\\/| | / /\\ \\ |  _  /  | |  |  ___/ / /\\ \\|  _  /|  <  ");
			Console.WriteLine("  ____) | |  | |/ ____ \\| | \\ \\  | |  | |    / ____ \\ | \\ \\| . \\ ");
			Console.WriteLine(" |_____/|_|  |_/_/    \\_\\_|  \\_\\ |_|  |_|   /_/    \\_\\_|  \\_\\_|\\_\\");
			Console.WriteLine("");
			Console.WriteLine("================================================================");
			Console.WriteLine("                     SISTEMA SMARTPARK                          ");
			Console.WriteLine("================================================================");
			Console.WriteLine("                          Inicio                                ");
			Console.WriteLine("================================================================");
			Console.ResetColor();

			// Solicitar Nombre del Operador
			Console.Write("Ingrese el nombre del operador: ");
			nombreOperador = Console.ReadLine();

			// Validar Código de Turno (Exactamente 4 caracteres)
			do
			{
				Console.Write("Ingrese el código de turno (4 caracteres): ");
				codigoTurno = Console.ReadLine();
				if (codigoTurno.Length != 4)
				{
					Console.ForegroundColor = ConsoleColor.DarkBlue; // Error en Azul Oscuro
					Console.WriteLine("Error: El código debe tener exactamente 4 caracteres.");
					Console.ResetColor();
				}
			} while (codigoTurno.Length != 4);

			// Capacidad del parqueo eliminada (Fijada en 10 automáticamente)
			Console.WriteLine("Capacidad del parqueo configurada automáticamente a 10 espacios.");

			// Inicializar Contadores
			ticketsCreados = 0;
			ticketsCerrados = 0;
			dineroRecaudado = 0.0;
			tiempoSimulado = 0;
			ticketActivo = false;

			Console.ForegroundColor = ConsoleColor.Green; // Confirmación en Verde
			Console.WriteLine("\nSistema inicializado correctamente. Presione cualquier tecla para continuar...");
			Console.ResetColor();
			Console.ReadKey();
			Console.Clear();

			// ==========================================
			// 2. MENÚ INTERACTIVO (CICLO PRINCIPAL)
			// ==========================================
			while (true)
			{
				// Mostrar Menú
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Blue; // Menú en Azul
				Console.WriteLine("================================================================");
				Console.WriteLine("   _____ __  __          _____ _______ _____         _____  _  __");
				Console.WriteLine("  / ____|  \\/  |   /\\   |  __ \\__   __|  __ \\   /\\  |  __ \\| |/ /");
				Console.WriteLine(" | (___ | \\  / |  /  \\  | |__) | | |  | |__) | /  \\ | |__) | ' / ");
				Console.WriteLine("  \\___ \\| |\\/| | / /\\ \\ |  _  /  | |  |  ___/ / /\\ \\|  _  /|  <  ");
				Console.WriteLine("  ____) | |  | |/ ____ \\| | \\ \\  | |  | |    / ____ \\ | \\ \\| . \\ ");
				Console.WriteLine(" |_____/|_|  |_/_/    \\_\\_|  \\_\\ |_|  |_|   /_/    \\_\\_|  \\_\\_|\\_\\");
				Console.WriteLine("");
				Console.WriteLine("================================================================");
				Console.WriteLine("                     SISTEMA SMARTPARK                          ");
				Console.WriteLine("================================================================");
				Console.WriteLine("                       MENÚ PRINCIPAL                           ");
				Console.WriteLine("================================================================");
				Console.ResetColor();

				Console.WriteLine($"Operador: {nombreOperador} | Turno: {codigoTurno}");
				Console.WriteLine($"Tiempo Simulado: {tiempoSimulado} minutos");
				Console.WriteLine("----------------------------------------------");
				Console.WriteLine("A. CREAR TICKET DE ENTRADA");
				Console.WriteLine("B. REGISTRAR SALIDA Y CALCULAR COBRO");
				Console.WriteLine("C. VER ESTADO DEL PARQUEO");
				Console.WriteLine("D. SIMULAR PASO DEL TIEMPO");
				Console.WriteLine("F. SALIR");
				Console.WriteLine("==============================================");

				Console.ForegroundColor = ConsoleColor.Green; // Indicador en Verde
				Console.Write("Seleccione una opción: ");
				Console.ResetColor();

				opcionMenu = Console.ReadLine().ToUpper();

				// Lógica de Opciones
				switch (opcionMenu)
				{
					// ---------------------------------------------------
					// A. CREAR TICKET DE ENTRADA
					// ---------------------------------------------------
					case "A":
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Blue; // Título en Azul
						Console.WriteLine("--- NUEVO INGRESO DE VEHÍCULO ---");
						Console.ResetColor();

						// Validar que no exista ticket activo
						if (ticketActivo)
						{
							Console.ForegroundColor = ConsoleColor.DarkBlue; // Error en Azul Oscuro
							Console.WriteLine("Error: Ya existe un ticket activo. Debe registrar la salida del vehículo actual primero.");
							Console.ResetColor();
						}
						else
						{
							// Solicitar Placa (6 a 8 caracteres, sin espacios)
							do
							{
								Console.Write("Ingrese la placa del vehículo (6-8 caracteres, sin espacios): ");
								placa = Console.ReadLine();
								if (placa.Length < 6 || placa.Length > 8 || placa.Contains(" "))
								{
									Console.ForegroundColor = ConsoleColor.DarkBlue;
									Console.WriteLine("Dato inválido. Intente de nuevo.");
									Console.ResetColor();
								}
								else
								{
									break;
								}
							} while (true);

							// Solicitar Tipo de Vehículo (Revertido a selección manual)
							do
							{
								Console.Write("Tipo de vehículo (1=Moto, 2=Auto, 3=Pickup/SUV): ");
								entradaValida = int.TryParse(Console.ReadLine(), out tipoVehiculo);
								if (!entradaValida || tipoVehiculo < 1 || tipoVehiculo > 3)
								{
									Console.ForegroundColor = ConsoleColor.DarkBlue;
									Console.WriteLine("Opción inválida. Ingrese 1, 2 o 3.");
									Console.ResetColor();
								}
							} while (!entradaValida || tipoVehiculo < 1 || tipoVehiculo > 3);

							// Solicitar Nombre del Cliente
							Console.Write("Nombre del cliente: ");
							nombreCliente = Console.ReadLine();

							// Solicitar si es VIP
							Console.Write("¿El cliente es VIP? (S/N): ");
							esVip = Console.ReadLine().ToUpper() == "S";

							// Guardar datos y activar ticket
							minutoEntrada = tiempoSimulado;
							ticketActivo = true;
							ticketsCreados++;

							Console.ForegroundColor = ConsoleColor.Green; // Éxito en Verde
							Console.WriteLine($"\n¡Ticket creado exitosamente!");
							Console.WriteLine($"Hora de entrada (simulada): {minutoEntrada} minutos.");
							Console.ResetColor();
						}
						Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
						Console.ReadKey();
						break;

					// ---------------------------------------------------
					// B. REGISTRAR SALIDA Y CALCULAR COBRO
					// ---------------------------------------------------
					case "B":
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Blue; // Título en Azul
						Console.WriteLine("--- REGISTRO DE SALIDA Y COBRO ---");
						Console.ResetColor();

						if (!ticketActivo)
						{
							Console.ForegroundColor = ConsoleColor.DarkBlue; // Error en Azul Oscuro
							Console.WriteLine("Error: No hay ningún ticket activo para procesar.");
							Console.ResetColor();
						}
						else
						{
							// 1. Calcular tiempo estacionado
							minutosEstacionados = tiempoSimulado - minutoEntrada;

							Console.WriteLine($"Tiempo total estacionado: {minutosEstacionados} minutos.");

							// 2. Determinar tarifa según PDF
							if (tipoVehiculo == 1) tarifaPorHora = 5.0;   // Moto
							else if (tipoVehiculo == 2) tarifaPorHora = 10.0; // Auto
							else tarifaPorHora = 15.0;                      // Pickup

							// 3. Calcular cobro base (Por fracción de hora)
							if (minutosEstacionados <= 15)
							{
								cobroBase = 0;
								horasCobradas = 0;
							}
							else
							{
								horasCobradas = Math.Ceiling(minutosEstacionados / 60.0);
								cobroBase = horasCobradas * tarifaPorHora;
							}

							Console.WriteLine($"Tarifa aplicada: Q{tarifaPorHora}/hora. Horas cobradas: {horasCobradas}.");
							Console.WriteLine($"Subtotal: Q{cobroBase}");

							// 4. Multa (> 6 horas) según PDF: Q25 fijos
							multa = 0;
							if (minutosEstacionados > 360) // 6 horas * 60 min
							{
								multa = 25.0;
								Console.ForegroundColor = ConsoleColor.DarkGreen; // Advertencia en Verde Oscuro
								Console.WriteLine($"¡Multa aplicada por tiempo extendido (>6h): Q{multa}");
								Console.ResetColor();
							}

							// 5. Descuento VIP según PDF: 10%
							descuento = 0;
							if (esVip)
							{
								descuento = (cobroBase + multa) * 0.10;
								Console.WriteLine($"Descuento VIP (10%): -Q{descuento}");
							}

							// Cálculo intermedio
							double subtotalConDescuento = (cobroBase + multa) - descuento;

							// 6. Recargo por permanencia extrema (> 12 horas) según PDF: 20%
							recargoPermanencia = 0;
							if (minutosEstacionados > 720) // 12 horas * 60 min
							{
								recargoPermanencia = subtotalConDescuento * 0.20;
								Console.ForegroundColor = ConsoleColor.DarkGreen; // Advertencia en Verde Oscuro
								Console.WriteLine($"¡Recargo por permanencia extrema (>12h): Q{recargoPermanencia}");
								Console.ResetColor();
							}

							// 7. Monto Final
							montoFinal = subtotalConDescuento + recargoPermanencia;

							// Actualizar sistema
							dineroRecaudado += montoFinal;
							ticketsCerrados++;

							// Mostrar resumen de pago
							Console.WriteLine("========================================");
							Console.ForegroundColor = ConsoleColor.Green; // Resultado final en Verde
							Console.WriteLine($"TOTAL A PAGAR: Q{montoFinal:F2}");
							Console.ResetColor();
							Console.WriteLine("========================================");

							// Liberar ticket
							ticketActivo = false;
							placa = "";
							nombreCliente = "";
							tipoVehiculo = 0;
							esVip = false;
						}
						Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
						Console.ReadKey();
						break;

					// ---------------------------------------------------
					// C. VER ESTADO DEL PARQUEO
					// ---------------------------------------------------
					case "C":
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Blue; // Título en Azul
						Console.WriteLine("--- ESTADO DEL PARQUEO ---");
						Console.ResetColor();

						int espaciosOcupados = (ticketActivo) ? 1 : 0;
						int espaciosDisponibles = capacidadTotal - espaciosOcupados;

						Console.WriteLine($"Capacidad Total: {capacidadTotal}");
						Console.WriteLine($"Espacios Ocupados: {espaciosOcupados}");
						Console.WriteLine($"Espacios Disponibles: {espaciosDisponibles}");
						Console.WriteLine($"----------------------------------------");
						Console.WriteLine($"Tiempo Simulado: {tiempoSimulado} minutos");
						Console.WriteLine($"Total Recaudado: Q{dineroRecaudado:F2}");
						Console.WriteLine($"Tickets Creados: {ticketsCreados}");
						Console.WriteLine($"Tickets Cerrados: {ticketsCerrados}");
						Console.WriteLine($"Tickets Activos: {espaciosOcupados}");

						Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
						Console.ReadKey();
						break;

					// ---------------------------------------------------
					// D. SIMULAR PASO DEL TIEMPO
					// ---------------------------------------------------
					case "D":
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Blue; // Título en Azul
						Console.WriteLine("--- SIMULAR TIEMPO ---");
						Console.ResetColor();

						// Solicitar minutos (1 a 1440) según PDF
						entradaValida = false;
						do
						{
							Console.Write("Ingrese minutos a avanzar (1-1440): ");
							entradaValida = int.TryParse(Console.ReadLine(), out minutosIngresados);
							if (!entradaValida || minutosIngresados < 1 || minutosIngresados > 1440)
							{
								Console.ForegroundColor = ConsoleColor.DarkBlue; // Error en Azul Oscuro
								Console.WriteLine("Valor inválido. Ingrese un número entre 1 y 1440.");
								Console.ResetColor();
							}
						} while (!entradaValida || minutosIngresados < 1 || minutosIngresados > 1440);

						// Sumar al tiempo actual
						tiempoSimulado += minutosIngresados;

						Console.ForegroundColor = ConsoleColor.Green; // Confirmación en Verde
						Console.WriteLine($"\nTiempo acumulado actual: {tiempoSimulado} minutos.");
						Console.ResetColor();

						// Advertencias según PDF
						if (ticketActivo)
						{
							int tiempoActualAuto = tiempoSimulado - minutoEntrada;

							if (tiempoActualAuto > 360 && tiempoActualAuto <= 720)
							{
								Console.ForegroundColor = ConsoleColor.DarkGreen; // Advertencia en Verde Oscuro
								Console.WriteLine("ADVERTENCIA: El vehículo actual supera las 6 horas. Multa próxima.");
								Console.ResetColor();
							}
							else if (tiempoActualAuto > 720)
							{
								Console.ForegroundColor = ConsoleColor.DarkGreen; // Advertencia en Verde Oscuro
								Console.WriteLine("ADVERTENCIA: El vehículo actual supera las 12 horas. Recargo por permanencia extrema.");
								Console.ResetColor();
							}
						}

						Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
						Console.ReadKey();
						break;

					// ---------------------------------------------------
					// F. SALIR
					// ---------------------------------------------------
					case "F":
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Blue; // Título en Azul
						Console.WriteLine("--- RESUMEN FINAL DEL TURNO ---");
						Console.ResetColor();
						Console.WriteLine($"Operador: {nombreOperador}");
						Console.WriteLine($"Código de Turno: {codigoTurno}");
						Console.WriteLine($"Tickets Creados: {ticketsCreados}");
						Console.WriteLine($"Tickets Cerrados: {ticketsCerrados}");
						Console.WriteLine($"Dinero Recaudado: Q{dineroRecaudado:F2}");

						Console.ForegroundColor = ConsoleColor.Green; // Mensaje final en Verde
						Console.WriteLine("\nGracias por usar SmartPark. ¡Hasta pronto!");
						Console.ResetColor();

						// Salir del ciclo (y del programa)
						return;

					default:
						Console.ForegroundColor = ConsoleColor.DarkBlue; // Error de opción en Azul Oscuro
						Console.WriteLine("\nOpción no válida. Intente nuevamente.");
						Console.ResetColor();
						Console.ReadKey();
						break;
				}
			}
		}
	}
}