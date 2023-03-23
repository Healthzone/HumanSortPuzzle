using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ReverseElement
{
    private List<GameObject> _bots;
    
    private FlaskController _previousFlask;

    public ReverseElement(List<GameObject> bots, FlaskController previousFlask)
    {
        _bots = bots;
        _previousFlask = previousFlask;
    }

    public List<GameObject> Bots { get => _bots; set => _bots = value; }
    public FlaskController PreviousFlask { get => _previousFlask; set => _previousFlask = value; }
}
