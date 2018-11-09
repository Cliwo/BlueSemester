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

public class IsDead : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        if (monController.IsDead())
        {
            Debug.Log("IsDead true");
            return true;
        }
        else
        {
            Debug.Log("IsDead false");
            return false;
        }
    }
}

public class Patrol : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        Debug.Log("Patrol true");
        monController.Patrol();
        return true;
    }
}

public class Death : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        Debug.Log("Death true");
        monController.Death();
        return true;
    }
}

public class InSight : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        if (monController.InSight())
        {
            Debug.Log("InSight true");
            return true;
        }
        else
        {
            Debug.Log("InSight false");
            return false;
        }
    }
}

public class Chase : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        Debug.Log("Chase true");
        monController.Chase();
        return true;
    }
}

public class IsDamaged : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        if (monController.IsDamaged())
        {
            Debug.Log("IsDamaged true");
            return true;
        }
        else
        {
            Debug.Log("IsDamaged false");
            return false;
        }
    }
}

public class InAttackRange : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        if (monController.InAttackRange())
        {
            Debug.Log("InAttackRange true");
            return true;
        }
        else
        {
            Debug.Log("InAttackRange false");
            return false;
        }
    }
}

public class Attack : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        Debug.Log("Attack true");
        monController.Attack();
        return true;
    }
}

public class Tornado : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        Debug.Log("Tornado true");
        monController.Tornado();
        return true;
    }
}

public class ThunderStroke : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        Debug.Log("ThunderStroke true");
        monController.ThunderStroke();
        return true;
    }
}

public class Wield : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        Debug.Log("Wield true");
        monController.Wield();
        return true;
    }
}

public class Pierce : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        Debug.Log("Pierce true");
        monController.Pierce();
        return true;
    }
}

public class Summon : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        Debug.Log("Summon true");
        monController.Summon();
        return true;
    }
}

public class IsCorrectSkill : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        if (monController.IsCorrectSkill())
        {
            Debug.Log("IsCorrectSkill true");
            return true;
        }
        else
        {
            Debug.Log("IsCorrectSkill false");
            return false;
        }
    }
}

public class IsRestOver : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        if (monController.IsRestOver())
        {
            Debug.Log("IsRestOver true");
            return true;
        }
        else
        {
            Debug.Log("IsRestOver false");
            return false;
        }
    }
}

public class RestInit : Node
{
    private MonsterController monController;

    public MonsterController MonController
    {
        set { monController = value; }
    }

    public override bool Invoke()
    {
        Debug.Log("RestInit true");
        monController.RestInit();
        return true;
    }
}