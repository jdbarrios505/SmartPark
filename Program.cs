using System;

namespace SmartPark
{
	class Program
	{
		static void Main(string[] args)
		{
			// ==========================================
			// DECLARACIÓN DE VARIABLES
			// ==========================================

			// Variables del Sistema - Información del operador y configuración del parqueo
			string nombreOperador = "";      // Nombre del operador del turno
			string codigoTurno = "";         // Código de turno de 4 caracteres
			int capacidadTotal = 0;          // Capacidad máxima del parqueo (mínimo 10)
			int ticketsCreados = 0;          // Contador de tickets creados durante el turno
			int ticketsCerrados = 0;         // Contador de tickets cerrados y pagados
			double dineroRecaudado = 0.0;    // Total de dinero recaudado en el turno
			int tiempoSimulado = 0;          // Tiempo simulado del sistema en minutos

			// Variables del Ticket Activo - Información del vehículo estacionado actualmente
			bool ticketActivo = false;       // Indica si hay un vehículo actualmente estacionado
			string placa = "";               // Placa del vehículo (6-8 caracteres)
			int tipoVehiculo = 0;            // Tipo de vehículo: 1=Moto, 2=Auto, 3=Pickup/SUV
			string nombreCliente = "";       // Nombre del cliente propietario del vehículo
			bool esVip = false;              // Indica si el cliente tiene estatus VIP
			int minutoEntrada = 0;           // Minuto de entrada del vehículo actual

			// Variables Auxiliares - Cálculos intermedios para tarifas y cobros
			string opcionMenu = "";          // Opción seleccionada por el usuario en el menú
			int minutosIngresados = 0;       // Minutos ingresados para simular paso del tiempo
			int minutosEstacionados = 0;     // Duración total del estacionamiento en minutos
			double tarifaPorHora = 0;        // Tarifa por hora según tipo de vehículo
			double horasCobradas = 0;        // Número de horas a cobrar (incluye fracciones)
			double cobroBase = 0;            // Cobro base sin multas ni descuentos
			double multa = 0;                // Multa por exceso de tiempo (>6 horas)
			double descuento = 0;            // Descuento VIP aplicado
			double recargoPermanencia = 0;   // Recargo adicional por permanencia prolongada (>12 horas)
			double montoFinal = 0;           // Monto final a pagar por el cliente
			bool entradaValida = false;      // Bandera para validar entradas numéricas

			// ==========================================
			// 1. REGISTRO INICIAL DEL SISTEMA
			// ==========================================
			// Configurar título de la ventana de consola
			Console.Title = "SmartPark - Sistema de Parqueo Inteligente";

			// Mostrar banner de bienvenida en color azul
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("================================================================");
			Console.WriteLine("   _____ __  __          _____ _______ _____         _____  _  __");
			Console.WriteLine("  / ____|  \\/  |   /\\   |  __ \\__   __|  __ \\   /\\  |  __ \\| |/ /");
			Console.WriteLine(" | (___ | \\  / |  /  \\  | |__) | | |  | |__) | /  \\ | |__) | ' / ");
			Console.WriteLine("  \\___ \\| |\\/| | / /\\ \\ |  _  /  | |  |  ___/ / /\\ \\|  _  /|  <  ");
			Console.WriteLine("  ____) | |  | |/ ____ \\| | \\ \\  | |  | |    / ____ \\ | \\ \\| . \\ ");
			Console.WriteLine(" |_____/|_|  |_/_/    \\_\\_|  \\_\\ |_|  |_|   /_/    \\_\\|  \\_\\_|\\_\\");
			Console.WriteLine("");
			Console.WriteLine("================================================================");
			Console.WriteLine("                     SISTEMA SMARTPARK                          ");
			Console.WriteLine("================================================================");
			Console.WriteLine("                          Inicio                                ");
			Console.WriteLine("================================================================");
			Console.ResetColor();

			// Solicitar nombre del operador
			Console.Write("Ingrese el nombre del operador: ");
			nombreOperador = Console.ReadLine();

			// Validar código de turno: debe tener exactamente 4 caracteres
			do
			{
				Console.Write("Ingrese el código de turno (4 caracteres): ");
				codigoTurno = Console.ReadLine();
				if (codigoTurno.Length != 4)
				{
					// Mostrar mensaje de error en color rojo
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error: El código debe tener exactamente 4 caracteres.");
					Console.ResetColor();
				}
			} while (codigoTurno.Length != 4);

			// Solicitar capacidad del parqueo: mínimo 10 espacios
			do
			{
				Console.Write("Ingrese la capacidad del parqueo (mínimo 10 espacios): ");
				// Intentar convertir entrada a número entero
				entradaValida = int.TryParse(Console.ReadLine(), out capacidadTotal);
				if (!entradaValida || capacidadTotal < 10)
				{
					// Mostrar mensaje de error si la entrada no es válida o es menor a 10
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error: La capacidad debe ser un número entero mayor o igual a 10.");
					Console.ResetColor();
				}
			} while (!entradaValida || capacidadTotal < 10);

			// Inicializar contadores y estado del sistema
			ticketsCreados = 0;
			ticketsCerrados = 0;
			dineroRecaudado = 0.0;
			tiempoSimulado = 0;
			ticketActivo = false;

			// Mostrar confirmación de inicialización en color verde
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"\nSistema inicializado correctamente.");
			Console.WriteLine($"Capacidad del parqueo: {capacidadTotal} espacios.");
			Console.WriteLine("Presione cualquier tecla para continuar...");
			Console.ResetColor();
			Console.ReadKey();
			Console.Clear();

			// ==========================================
			// 2. MENÚ INTERACTIVO (CICLO PRINCIPAL)
			// ==========================================
			// Ciclo infinito que continúa hasta que el usuario selecciona "Salir"
			while (true)
			{
				// Limpiar pantalla y mostrar menú principal
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine("================================================================");
				Console.WriteLine("   _____ __  __          _____ _______ _____         _____  _  __");
				Console.WriteLine("  / ____|  \\/  |   /\\   |  __ \\__   __|  __ \\   /\\  |  __ \\| |/ /");
				Console.WriteLine(" | (___ | \\  / |  /  \\  | |__) | | |  | |__) | /  \\ | |__) | ' / ");
				Console.WriteLine("  \\___ \\| |\\/| | / /\\ \\ |  _  /  | |  |  ___/ / /\\ \\|  _  /|  <  ");
				Console.WriteLine("  ____) | |  | |/ ____ \\| | \\ \\  | |  | |    / ____ \\ | \\ \\| . \\ ");
				Console.WriteLine(" |_____/|_|  |_/_/    \\_\\_|  \\_\\ |_|  |_|   /_/    \\_\\|  \\_\\_|\\_\\");
				Console.WriteLine("");
				Console.WriteLine("================================================================");
				Console.WriteLine("                     SISTEMA SMARTPARK                          ");
				Console.WriteLine("================================================================");
				Console.WriteLine("                       MENÚ PRINCIPAL                           ");
				Console.WriteLine("================================================================");
				Console.ResetColor();

				// Calcular espacios disponibles para mostrar estado actual del parqueo
				int espaciosOcupados = (ticketActivo) ? 1 : 0;
				int espaciosDisponibles = capacidadTotal - espaciosOcupados;

				// Mostrar información actual del sistema
				Console.WriteLine($"Operador: {nombreOperador} | Turno: {codigoTurno}");
				Console.WriteLine($"Tiempo Simulado: {tiempoSimulado} minutos | Espacios Disponibles: {espaciosDisponibles}/{capacidadTotal}");
				Console.WriteLine("----------------------------------------------");
				Console.WriteLine("A. CREAR TICKET DE ENTRADA");
				Console.WriteLine("B. REGISTRAR SALIDA Y CALCULAR COBRO");
				Console.WriteLine("C. VER ESTADO DEL PARQUEO");
				Console.WriteLine("D. SIMULAR PASO DEL TIEMPO");
				Console.WriteLine("E. SALIR");
				Console.WriteLine("==============================================");

				// Solicitar opción del usuario
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("Seleccione una opción: ");
				Console.ResetColor();

				opcionMenu = Console.ReadLine().ToUpper();

				// Procesar opción seleccionada
				switch (opcionMenu)
				{
					// ---------------------------------------------------
					// OPCIÓN A: CREAR TICKET DE ENTRADA
					// ---------------------------------------------------
					case "A":
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.WriteLine("--- NUEVO INGRESO DE VEHÍCULO ---");
						Console.ResetColor();

						// Recalcular espacios disponibles
						espaciosOcupados = (ticketActivo) ? 1 : 0;
						espaciosDisponibles = capacidadTotal - espaciosOcupados;

						// Validar que no exista un ticket activo
						if (ticketActivo)
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("Error: Ya existe un ticket activo. Debe registrar la salida del vehículo actual primero.");
							Console.ResetColor();
						}
						// Validar que el parqueo no esté lleno
						else if (espaciosDisponibles == 0)
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("Error: El parqueo está lleno. No se puede crear un nuevo ticket en este momento.");
							Console.ResetColor();
						}
						else
						{
							// Solicitar placa del vehículo: 6 a 8 caracteres, sin espacios
							do
							{
								Console.Write("Ingrese la placa del vehículo (6-8 caracteres, sin espacios): ");
								placa = Console.ReadLine();
								if (placa.Length < 6 || placa.Length > 8 || placa.Contains(" "))
								{
									Console.ForegroundColor = ConsoleColor.Red;
									Console.WriteLine("Dato inválido. Intente de nuevo.");
									Console.ResetColor();
								}
								else
								{
									break;
								}
							} while (true);

							// Solicitar tipo de vehículo: 1=Moto, 2=Auto, 3=Pickup/SUV
							do
							{
								Console.Write("Tipo de vehículo (1=Moto, 2=Auto, 3=Pickup/SUV): ");
								entradaValida = int.TryParse(Console.ReadLine(), out tipoVehiculo);
								if (!entradaValida || tipoVehiculo < 1 || tipoVehiculo > 3)
								{
									Console.ForegroundColor = ConsoleColor.Red;
									Console.WriteLine("Opción inválida. Ingrese 1, 2 o 3.");
									Console.ResetColor();
								}
							} while (!entradaValida || tipoVehiculo < 1 || tipoVehiculo > 3);

							// Solicitar nombre del cliente
							Console.Write("Nombre del cliente: ");
							nombreCliente = Console.ReadLine();

							// Preguntar si el cliente tiene estatus VIP
							Console.Write("¿El cliente es VIP? (S/N): ");
							esVip = Console.ReadLine().ToUpper() == "S";

							// Guardar información del ticket y activarlo
							minutoEntrada = tiempoSimulado;
							ticketActivo = true;
							ticketsCreados++;

							// Mostrar confirmación en color verde
							Console.ForegroundColor = ConsoleColor.Green;
							Console.WriteLine($"\n¡Ticket creado exitosamente!");
							Console.WriteLine($"Placa: {placa}");
							Console.WriteLine($"Hora de entrada (simulada): {minutoEntrada} minutos.");
							Console.ResetColor();
						}
						Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
						Console.ReadKey();
						break;

					// ---------------------------------------------------
					// OPCIÓN B: REGISTRAR SALIDA Y CALCULAR COBRO
					// ---------------------------------------------------
					case "B":
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.WriteLine("--- REGISTRO DE SALIDA Y COBRO ---");
						Console.ResetColor();

						// Validar que exista un ticket activo para procesar
						if (!ticketActivo)
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("Error: No hay ningún ticket activo para procesar.");
							Console.ResetColor();
						}
						else
						{
							// Calcular tiempo total estacionado en minutos
							minutosEstacionados = tiempoSimulado - minutoEntrada;

							Console.WriteLine($"Placa: {placa}");
							Console.WriteLine($"Cliente: {nombreCliente}");
							Console.WriteLine($"Tiempo total estacionado: {minutosEstacionados} minutos.");

							// Determinar tarifa por hora según tipo de vehículo
							if (tipoVehiculo == 1) tarifaPorHora = 5.0;      // Moto: Q5/hora
							else if (tipoVehiculo == 2) tarifaPorHora = 10.0; // Auto: Q10/hora
							else tarifaPorHora = 15.0;                       // Pickup: Q15/hora

							// Calcular cobro base considerando:
							// - Gratuidad para estacionamientos de 15 minutos o menos
							// - Cobro por fracción de hora para estacionamientos mayores
							if (minutosEstacionados <= 15)
							{
								cobroBase = 0;
								horasCobradas = 0;
							}
							else
							{
								// Math.Ceiling redondea hacia arriba para cobrar por cada fracción de hora
								horasCobradas = Math.Ceiling(minutosEstacionados / 60.0);
								cobroBase = horasCobradas * tarifaPorHora;
							}

							Console.WriteLine($"Tarifa aplicada: Q{tarifaPorHora}/hora. Horas cobradas: {horasCobradas}.");
							Console.WriteLine($"Subtotal: Q{cobroBase}");

							// Aplicar multa si el estacionamiento excede 6 horas (360 minutos)
							multa = 0;
							if (minutosEstacionados > 360)
							{
								multa = 25.0;
								Console.ForegroundColor = ConsoleColor.Yellow;
								Console.WriteLine($"¡Multa aplicada por tiempo extendido (>6h): Q{multa}");
								Console.ResetColor();
							}

							// Aplicar descuento VIP del 10% sobre cobro base + multa
							descuento = 0;
							if (esVip)
							{
								descuento = (cobroBase + multa) * 0.10;
								Console.ForegroundColor = ConsoleColor.Yellow;
								Console.WriteLine($"Descuento VIP (10%): -Q{descuento:F2}");
								Console.ResetColor();
							}

							// Calcular subtotal después de aplicar descuento VIP
							double subtotalConDescuento = (cobroBase + multa) - descuento;

							// Aplicar recargo por permanencia extrema si excede 12 horas (720 minutos)
							// El recargo es del 20% sobre el monto después de descuentos
							recargoPermanencia = 0;
							if (minutosEstacionados > 720)
							{
								recargoPermanencia = subtotalConDescuento * 0.20;
								Console.ForegroundColor = ConsoleColor.Yellow;
								Console.WriteLine($"¡Recargo por permanencia extrema (>12h): Q{recargoPermanencia:F2}");
								Console.ResetColor();
							}

							// Calcular monto final a pagar
							montoFinal = subtotalConDescuento + recargoPermanencia;

							// Actualizar registros del sistema
							dineroRecaudado += montoFinal;
							ticketsCerrados++;

							// Mostrar resumen de pago
							Console.WriteLine("========================================");
							Console.ForegroundColor = ConsoleColor.Green;
							Console.WriteLine($"TOTAL A PAGAR: Q{montoFinal:F2}");
							Console.ResetColor();
							Console.WriteLine("========================================");

							// Liberar ticket: reiniciar variables del vehículo activo
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
					// OPCIÓN C: VER ESTADO DEL PARQUEO
					// ---------------------------------------------------
					case "C":
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.WriteLine("--- ESTADO DEL PARQUEO ---");
						Console.ResetColor();

						// Calcular espacios ocupados y disponibles
						espaciosOcupados = (ticketActivo) ? 1 : 0;
						espaciosDisponibles = capacidadTotal - espaciosOcupados;

						// Mostrar estado actual del sistema
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
					// OPCIÓN D: SIMULAR PASO DEL TIEMPO
					// ---------------------------------------------------
					case "D":
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.WriteLine("--- SIMULAR TIEMPO ---");
						Console.ResetColor();

						// Solicitar número de minutos a avanzar: entre 1 y 1440 (24 horas)
						entradaValida = false;
						do
						{
							Console.Write("Ingrese minutos a avanzar (1-1440): ");
							entradaValida = int.TryParse(Console.ReadLine(), out minutosIngresados);
							if (!entradaValida || minutosIngresados < 1 || minutosIngresados > 1440)
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine("Valor inválido. Ingrese un número entre 1 y 1440.");
								Console.ResetColor();
							}
						} while (!entradaValida || minutosIngresados < 1 || minutosIngresados > 1440);

						// Sumar minutos ingresados al tiempo simulado total
						tiempoSimulado += minutosIngresados;

						// Mostrar confirmación
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine($"\nTiempo acumulado actual: {tiempoSimulado} minutos.");
						Console.ResetColor();

						// Mostrar advertencias si hay un ticket activo y se exceden límites de tiempo
						if (ticketActivo)
						{
							// Calcular tiempo que lleva estacionado el vehículo actual
							int tiempoActualAuto = tiempoSimulado - minutoEntrada;

							// Advertencia si excede 6 horas pero aún no llega a 12
							if (tiempoActualAuto > 360 && tiempoActualAuto <= 720)
							{
								Console.ForegroundColor = ConsoleColor.Yellow;
								Console.WriteLine("ADVERTENCIA: El vehículo actual supera las 6 horas. Multa próxima.");
								Console.ResetColor();
							}
							// Advertencia si excede 12 horas
							else if (tiempoActualAuto > 720)
							{
								Console.ForegroundColor = ConsoleColor.Yellow;
								Console.WriteLine("ADVERTENCIA: El vehículo actual supera las 12 horas. Recargo por permanencia extrema aplicable.");
								Console.ResetColor();
							}
						}

						Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
						Console.ReadKey();
						break;

					// ---------------------------------------------------
					// OPCIÓN E: SALIR DEL SISTEMA
					// ---------------------------------------------------
					case "E":
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.WriteLine("--- RESUMEN FINAL DEL TURNO ---");
						Console.ResetColor();

						// Mostrar resumen completo del turno
						Console.WriteLine($"Operador: {nombreOperador}");
						Console.WriteLine($"Código de Turno: {codigoTurno}");
						Console.WriteLine($"Capacidad del Parqueo: {capacidadTotal}");
						Console.WriteLine($"Tickets Creados: {ticketsCreados}");
						Console.WriteLine($"Tickets Cerrados: {ticketsCerrados}");
						Console.WriteLine($"Dinero Recaudado: Q{dineroRecaudado:F2}");

						// Mostrar mensaje de despedida
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("\nGracias por usar SmartPark. ¡Hasta pronto!");
						Console.ResetColor();

						// Terminar el programa
						return;

					// ---------------------------------------------------
					// OPCIÓN INVÁLIDA
					// ---------------------------------------------------
					default:
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("\nOpción no válida. Intente nuevamente.");
						Console.ResetColor();
						Console.ReadKey();
						break;
				}
			}
		}
	}
}