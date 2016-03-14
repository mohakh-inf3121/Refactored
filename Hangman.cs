using System;


public class Hangman
{
	static void Main()
	{
		ScoreBoard scoreBoard = new ScoreBoard();
		besenica game = new besenica();
		Console.WriteLine("Welcome to “Hangman” game. Please try to guess my secret word.");
		string command = null;
		do
		{
			Console.WriteLine();
			game.PrintCurrentProgress();
			checkIfOver(command, scoreBoard, game);
		} while (command != "exit");
	}

	static void ExecuteCommand(string command, ScoreBoard scoreBoard, besenica game)
	{
		switch (command)
		{
			case "top":
				{
					scoreBoard.Print();
				}
				break;
			case "help":
				{
					char revealedLetter = game.RevealALetter();
					Console.WriteLine("OK, I reveal for you the next letter '{0}'.", revealedLetter);
				}
				break;
			case "restart":
				{
					scoreBoard.ReSet();
					Console.WriteLine("\nWelcome to “Hangman” game. Please try to guess my secret word.");
					game.ReSet();
				}
				break;
			case "exit":
				{
					Console.WriteLine("Good bye!");
					return;
				}
			default:
				{
					Console.WriteLine("Incorrect guess or command!");
				}
				break;
		}
	}
	
	static void  checkIfOver(string command, ScoreBoard scoreBoard, besenica game) { 

	   if (game.isOver()) {
			gameIsOver(scoreBoard, game);
		}
		else {
			Console.Write("Enter your guess: ");
			command = Console.ReadLine();
			command.ToLower();
			
			checkLetter(command, scoreBoard, game);
		}
	}
	
	static void checkLetter(string command, ScoreBoard scoreBoard, besenica game) {
		if (command.Length == 1) {
			int occuranses = game.NumberOccuranceOfLetter(command[0]);
			giveFeedback(occuranses, command);
		}
		else {
			ExecuteCommand(command, scoreBoard, game);
		}
	}
	
	static void giveFeedback(int occuranses, string command) {
		 if (occuranses == 0) {
				Console.WriteLine("Sorry! There are no unrevealed letters “{0}”.", command[0]);
			}
			else {
				Console.WriteLine("Good job! You revealed {0} letter(s).", occuranses);
			}
	}

	static void gameIsOver(ScoreBoard scoreBoard, besenica game) {
		if (game.HelpUsed)  {
			Console.WriteLine("You won with {0} mistake(s) but you have cheated." +
							" You are not allowed to enter into the scoreboard.", game.Mistackes);
		}
		else {
			noCheating(scoreBoard, game);
		}
		
		game.ReSet();
	}
	
	static void noCheating(ScoreBoard scoreBoard, besenica game) {
		if (scoreBoard.GetWorstTopScore() <= game.Mistackes) {
				Console.WriteLine("You won with {0} mistake(s) but you score did not enter in the scoreboard", game.Mistackes);
			}
			else {
				Console.Write("Please enter your name for the top scoreboard: ");
				string name = Console.ReadLine();	
				scoreBoard.AddNewScore(name, game.Mistackes);
				scoreBoard.Print();
			}
	}
}
