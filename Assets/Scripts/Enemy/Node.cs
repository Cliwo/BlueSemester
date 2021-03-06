﻿using System.Collections;
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

// TODO : 패턴이 다 똑같아서 추후에 수정하도록 한다

public class IsDead : Node
{
    private MonsterController monController;

    public IsDead(MonsterController monster)
    {
        monController = monster;
    }

    public override bool Invoke()
    {
        if (monController.IsDead())
        {
            //Debug.Log("IsDead true");
            return true;
        }
        else
        {
            //Debug.Log("IsDead false");
            return false;
        }
    }
}

public class Patrol : Node
{
    private MonsterController monController;

    public Patrol(MonsterController monster)
    {
        monController = monster;
    }

    public override bool Invoke()
    {
        //Debug.Log("Patrol true");
        monController.Patrol();
        return true;
    }
}

public class Death : Node
{
    private MonsterController monController;

    public Death(MonsterController monster)
    {
        monController = monster;
    }

    public override bool Invoke()
    {
        //Debug.Log("Death true");
        monController.Death();
        return true;
    }
}

public class InSight : Node
{
    private MonsterController monController;

    public InSight(MonsterController monster)
    {
        monController = monster;
    }

    public override bool Invoke()
    {
        if (monController.InSight())
        {
            //Debug.Log("InSight true");
            return true;
        }
        else
        {
            //Debug.Log("InSight false");
            return false;
        }
    }
}

public class Chase : Node
{
    private MonsterController monController;

    //public MonsterController MonController
    //{
    //    set { monController = value; }
    //}

    public Chase(MonsterController monster)
    {
        monController = monster;
    }

    public override bool Invoke()
    {
        //Debug.Log("Chase true");
        monController.Chase();
        return true;
    }
}

public class IsDamaged : Node
{
    private MonsterController monController;

    public IsDamaged(MonsterController monster)
    {
        monController = monster;
    }

    public override bool Invoke()
    {
        if (monController.IsDamaged())
        {
            //Debug.Log("IsDamaged true");
            return true;
        }
        else
        {
            //Debug.Log("IsDamaged false");
            return false;
        }
    }
}

public class InAttackRange : Node
{
    private MonsterController monController;

    public InAttackRange(MonsterController monster)
    {
        monController = monster;
    }

    public override bool Invoke()
    {
        if (monController.InAttackRange())
        {
            //Debug.Log("InAttackRange true");
            return true;
        }
        else
        {
            //Debug.Log("InAttackRange false");
            return false;
        }
    }
}

public class Attack : Node
{
    private MonsterController monController;

    public Attack(MonsterController monster)
    {
        monController = monster;
    }

    public override bool Invoke()
    {
        //Debug.Log("Attack true");
        monController.Attack();
        return true;
    }
}