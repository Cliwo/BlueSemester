using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    public abstract bool Invoke();
}

public class CompositeNode : Node
{
    private Stack<Node> childrens = new Stack<Node>();

    public override bool Invoke()
    {
        throw new System.NotImplementedException();
    }

    public void AddChild(Node node)
    {
        childrens.Push(node);
    }

    public Stack<Node> GetChildrens()
    {
        return childrens;
    }
}

public class Selector : CompositeNode
{
    public override bool Invoke()
    {
        foreach (var node in GetChildrens())
        {
            if (node.Invoke())
            {
                return true;
            }
        }
        return false;
    }
}

public class Sequence : CompositeNode
{
    public override bool Invoke()
    {
        foreach (var node in GetChildrens())
        {
            if (!node.Invoke())
            {
                return false;
            }
        }
        return true;
    }
}