using System;
using System.Linq;
using System.Collections.Generic;

namespace ExpressionEvaluation {
	abstract class Node { 
		public Node? left { get; set; }
    }

    abstract class OperatorNode : Node {
        public abstract int? EvaluateInt();
        public abstract double? EvaluateDouble();
    }

    class BinaryOperatorNode : OperatorNode {
        public Node? right { get; set; }

        public override int? EvaluateInt() => 0;
        public override double? EvaluateDouble() => 0;
    }

    class UnaryOperatorNode : OperatorNode {
        public override int? EvaluateInt() => 0;
        public override double? EvaluateDouble() => 0;
    }

    class PlusOperatorNode : BinaryOperatorNode {
        public override int? EvaluateInt() {
            int? leftValue = 0;
            int? rightValue = 0;
            if (this.left is OperatorNode leftOperatorNode) leftValue = leftOperatorNode.EvaluateInt();
            else if (this.left is ValueNode leftValueNode) leftValue = leftValueNode.intValue;
            if (this.right is OperatorNode rightOperatorNode) rightValue = rightOperatorNode.EvaluateInt();
            else if (this.right is ValueNode rightValueNode) rightValue = rightValueNode.intValue;
            return checked(leftValue + rightValue);
        }

        public override double? EvaluateDouble() {
            double? leftValue = 0;
            double? rightValue = 0;
            if (this.left is OperatorNode leftOperatorNode) leftValue = leftOperatorNode.EvaluateDouble();
            else if (this.left is ValueNode leftValueNode) leftValue = leftValueNode.doubleValue;
            if (this.right is OperatorNode rightOperatorNode) rightValue = rightOperatorNode.EvaluateDouble();
            else if (this.right is ValueNode rightValueNode) rightValue = rightValueNode.doubleValue;
            return leftValue + rightValue;
        }
    }

    class MinusOperatorNode : BinaryOperatorNode {
		public override int? EvaluateInt() {
            int? leftValue = 0;
            int? rightValue = 0;
            if (this.left is OperatorNode leftOperatorNode) leftValue = leftOperatorNode.EvaluateInt();
            else if (this.left is ValueNode leftValueNode) leftValue = leftValueNode.intValue;
            if (this.right is OperatorNode rightOperatorNode) rightValue = rightOperatorNode.EvaluateInt();
            else if (this.right is ValueNode rightValueNode) rightValue = rightValueNode.intValue;
            return checked(leftValue - rightValue);
        }

        public override double? EvaluateDouble() {
            double? leftValue = 0;
            double? rightValue = 0;
            if (this.left is OperatorNode leftOperatorNode) leftValue = leftOperatorNode.EvaluateDouble();
            else if (this.left is ValueNode leftValueNode) leftValue = leftValueNode.doubleValue;
            if (this.right is OperatorNode rightOperatorNode) rightValue = rightOperatorNode.EvaluateDouble();
            else if (this.right is ValueNode rightValueNode) rightValue = rightValueNode.doubleValue;
            return leftValue - rightValue;
        }
    }

    class MultiplicationOperatorNode : BinaryOperatorNode {
		public override int? EvaluateInt() {
            int? leftValue = 0;
            int? rightValue = 0;
            if (this.left is OperatorNode leftOperatorNode) leftValue = leftOperatorNode.EvaluateInt();
            else if (this.left is ValueNode leftValueNode) leftValue = leftValueNode.intValue;
            if (this.right is OperatorNode rightOperatorNode) rightValue = rightOperatorNode.EvaluateInt();
            else if (this.right is ValueNode rightValueNode) rightValue = rightValueNode.intValue;
            return checked(leftValue * rightValue);
        }

        public override double? EvaluateDouble() {
            double? leftValue = 0;
            double? rightValue = 0;
            if (this.left is OperatorNode leftOperatorNode) leftValue = leftOperatorNode.EvaluateDouble();
            else if (this.left is ValueNode leftValueNode) leftValue = leftValueNode.doubleValue;
            if (this.right is OperatorNode rightOperatorNode) rightValue = rightOperatorNode.EvaluateDouble();
            else if (this.right is ValueNode rightValueNode) rightValue = rightValueNode.doubleValue;
            return leftValue * rightValue;
        }
    }
    
    class DivisionOperatorNode : BinaryOperatorNode {
        public override int? EvaluateInt() {
            int? leftValue = 0;
            int? rightValue = 0;
            if (this.left is OperatorNode leftOperatorNode) leftValue = leftOperatorNode.EvaluateInt();
            else if (this.left is ValueNode leftValueNode) leftValue = leftValueNode.intValue;
            if (this.right is OperatorNode rightOperatorNode) rightValue = rightOperatorNode.EvaluateInt();
            else if (this.right is ValueNode rightValueNode) rightValue = rightValueNode.intValue;
            return checked(leftValue / rightValue);
        }

        public override double? EvaluateDouble() {
            double? leftValue = 0;
            double? rightValue = 0;
            if (this.left is OperatorNode leftOperatorNode) leftValue = leftOperatorNode.EvaluateDouble();
            else if (this.left is ValueNode leftValueNode) leftValue = leftValueNode.doubleValue;
            if (this.right is OperatorNode rightOperatorNode) rightValue = rightOperatorNode.EvaluateDouble();
            else if (this.right is ValueNode rightValueNode) rightValue = rightValueNode.doubleValue;
            return leftValue / rightValue;
        }
    }

    class NegationOperatorNode : UnaryOperatorNode {
        public override int? EvaluateInt() {
            int? leftValue = 0;
            if (this.left is OperatorNode leftOperatorNode) leftValue = leftOperatorNode.EvaluateInt();
            else if (this.left is ValueNode leftValueNode) leftValue = leftValueNode.intValue;
            return -leftValue;
        }

        public override double? EvaluateDouble() {
            double? leftValue = 0;
            if (this.left is OperatorNode leftOperatorNode) leftValue = leftOperatorNode.EvaluateDouble();
            else if (this.left is ValueNode leftValueNode) leftValue = leftValueNode.doubleValue;
            return -leftValue;
        }
    }

    class ValueNode : Node { 
        public int? intValue { get; set; }
        public double? doubleValue { get; set; }
    }

    class ExpressionTree {
        List<string> expression = new List<string>();
        Node? root;

        public ExpressionTree() { }

        public ExpressionTree(string expression) { 
            this.expression = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        }
	
		int processingIndex = 0;

        public void BuildTree() {
            if (expression.Count > 1) {
                expression.RemoveAt(0);
                if (ExpressionIsValid()) root = BuildSubTree(expression[processingIndex]);
                else {
                    root = null;
                    throw new ArgumentException("Format Error");
                }
            }
            else {
                root = null;
                throw new ArgumentException("Format Error");
            }
        }

        bool ExpressionIsValid() {
            int operators = 0;
            int operands = 0;
            for (int i = expression.Count - 1; i >= 0; i--) {
                if (int.TryParse(expression[i], out int num)) operands++;
                else if (expression[i][0] == '~' && operands > 0) continue;
                else if (expression[i].Length == 1 && binaryOperators.Contains(expression[i][0])) operators++;
                else return false;
                if (operands > 1 && operators > 0) {
                    operators--;
                    operands--;
                }
            }
            if (operands != 1 || operators != 0) return false;
            return true;
        }

        char[] binaryOperators = new char[] { '+', '-', '*', '/' };

        Node BuildSubTree(string token) {          
            if (binaryOperators.Contains(token[0])) {
                BinaryOperatorNode node = new BinaryOperatorNode();
                if (token == "+") node = new PlusOperatorNode();
                else if (token == "-") node = new MinusOperatorNode();
                else if (token == "*") node = new MultiplicationOperatorNode();
                else if (token == "/") node = new DivisionOperatorNode();
                if (processingIndex + 1 < expression.Count) node.left = BuildSubTree(expression[++processingIndex]);
                if (processingIndex + 1 < expression.Count) node.right = BuildSubTree(expression[++processingIndex]);
				return node;
            }
            else if (token == "~") {
                UnaryOperatorNode node = new NegationOperatorNode();
                if (processingIndex + 1 < expression.Count) node.left = BuildSubTree(expression[++processingIndex]);
				return node;
            }
            else {
                int.TryParse(token, out int num);
                ValueNode node = new ValueNode() { intValue = num, doubleValue = (double) num };
                return node;
            }
        }

        public int? EvaluateTreeInt() {
            if (root is OperatorNode operatorNode) return operatorNode.EvaluateInt();
            else if (root is ValueNode valueNode) return valueNode.intValue;
            else throw new ArgumentException("Expression Missing");
        }

        public double? EvaluateTreeDouble() {
            if (root is OperatorNode operatorNode) return operatorNode.EvaluateDouble();
            else if (root is ValueNode valueNode) return valueNode.doubleValue;
            else throw new ArgumentException("Expression Missing");
        }
    }

    class Program {
        static void Main(string[] args) {
            string? expression;
            ExpressionTree tree = new ExpressionTree();
            while ((expression = Console.ReadLine()) is not null && expression != "end") {
                if (expression.Length > 0 && expression[0] == '=') {
                    tree = new ExpressionTree(expression);
                    try {
                        tree.BuildTree();
                    }
                    catch (ArgumentException exception) {
                        Console.WriteLine(exception.Message);
                    }
                }
                else if (expression == "i") {
                    try {
                        Console.WriteLine(tree.EvaluateTreeInt());
                    }
                    catch (OverflowException) {
                        Console.WriteLine("Overflow Error");
                    }
                    catch (DivideByZeroException) {
                        Console.WriteLine("Divide Error");
                    }
                    catch (ArgumentException exception) {
                        Console.WriteLine(exception.Message);
                    }
                }
                else if (expression == "d") {
                    try {
                        Console.WriteLine(string.Format("{0:0.00000}", tree.EvaluateTreeDouble()));
                    }
                    catch (ArgumentException exception) {
                        Console.WriteLine(exception.Message);
                    }
                }
                else if (expression != "") Console.WriteLine("Format Error");
            }
        }
    }
}