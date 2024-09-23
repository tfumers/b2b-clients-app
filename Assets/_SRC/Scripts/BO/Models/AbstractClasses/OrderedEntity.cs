using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OrderedEntity
{
    protected long id;

    protected int orderNumber;

    protected OrderedEntity(long id, int orderNumber)
    {
        this.id = id;
        this.orderNumber = orderNumber;
    }

    protected OrderedEntity()
    {

    }

    public long Id { get => id; set => id = value; }
    public int OrderNumber { get => orderNumber; set => orderNumber = value; }
}
