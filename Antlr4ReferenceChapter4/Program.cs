using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace AntlrChapter4
{
	class Program
	{
		static void Main(string[] args)
		{
			ExampleTwo();
		}

		static void ExampleTwo()
		{
			AntlrInputStream input = new AntlrInputStream(File.Open("t.expr", FileMode.Open));

			ExprLexer lexer = new ExprLexer(input);
			CommonTokenStream tokenStream = new CommonTokenStream(lexer);
			ExprParser parser = new ExprParser(tokenStream);

			IParseTree tree = parser.prog();
			MyExprVisitor visitor = new MyExprVisitor();
			visitor.Visit(tree);
			Console.Read();
		}

		static void ExampleOne()
		{
			AntlrInputStream input = new AntlrInputStream(File.Open("t.expr", FileMode.Open));

			ExprLexer lexer = new ExprLexer(input);
			CommonTokenStream tokenStream = new CommonTokenStream(lexer);
			ExprParser parser = new ExprParser(tokenStream);

			IParseTree tree = parser.prog();

			string output = tree.ToStringTree(parser).Replace("\\n", "\n");//Environment.NewLine);
			Console.Write(output);
			Console.Read();
		}
	}

	public class MyExprVisitor : ExprBaseVisitor<int>
	{
		public override int VisitAssign(ExprParser.AssignContext context)
		{
			string id = context.ID().GetText();
			int value = Visit(context.expr());
			Console.WriteLine("{0} = {1}", id, value);
			return value;
		}

		public override int VisitInt(ExprParser.IntContext context)
		{
			return int.Parse(context.INT().GetText());
		}
	}
}
