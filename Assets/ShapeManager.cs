using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Base class for shapes demonstrating inheritance and encapsulation
public class Shape : MonoBehaviour
{
    private string shapeName; // ENCAPSULATION

    public string ShapeName
    {
        get { return shapeName; } // ENCAPSULATION
        set { shapeName = value; } // ENCAPSULATION
    }

    public virtual void DisplayText() // POLYMORPHISM
    {
        Debug.Log($"This is a {ShapeName}!");
    }

    public void Initialize(string name) // ABSTRACTION
    {
        ShapeName = name;
    }
}

// Sphere class inheriting from Shape
public class Sphere : Shape // INHERITANCE
{
    public override void DisplayText() // POLYMORPHISM
    {
        Debug.Log("You clicked on a Sphere. It's round!");
    }
}

// Cube class inheriting from Shape
public class Cube : Shape // INHERITANCE
{
    public override void DisplayText() // POLYMORPHISM
    {
        Debug.Log("You clicked on a Cube. It's square!");
    }
}

// Cylinder class inheriting from Shape
public class Cylinder : Shape // INHERITANCE
{
    public override void DisplayText() // POLYMORPHISM
    {
        Debug.Log("You clicked on a Cylinder. It's tall and round!");
    }
}

public class ShapeManager : MonoBehaviour
{
    public Text messageText;

    void Start()
    {
        CreateAndDisplayShape<Sphere>("Sphere", new Vector3(-2, 0, 0), Color.red);
        CreateAndDisplayShape<Cube>("Cube", new Vector3(0, 0, 0), Color.blue);
        CreateAndDisplayShape<Cylinder>("Cylinder", new Vector3(2, 0, 0), Color.green);
    }

    void CreateAndDisplayShape<T>(string shapeName, Vector3 position, Color color) where T : Shape // ABSTRACTION
    {
        GameObject shapeObj = GameObject.CreatePrimitive(GetPrimitiveType(typeof(T)));
        shapeObj.transform.position = position;

        Shape shape = shapeObj.AddComponent<T>();
        shape.Initialize(shapeName);
        shapeObj.GetComponent<Renderer>().material.color = color;

        shapeObj.AddComponent<BoxCollider>(); // Add collider for click detection
        shapeObj.AddComponent<ClickableShape>().Initialize(shape, messageText); // Link click logic
    }

    PrimitiveType GetPrimitiveType(System.Type type)
    {
        if (type == typeof(Sphere)) return PrimitiveType.Sphere;
        if (type == typeof(Cube)) return PrimitiveType.Cube;
        if (type == typeof(Cylinder)) return PrimitiveType.Cylinder;
        return PrimitiveType.Cube; // Default
    }
}

public class ClickableShape : MonoBehaviour
{
    private Shape shape;
    private Text messageText;

    public void Initialize(Shape shape, Text messageText)
    {
        this.shape = shape;
        this.messageText = messageText;
    }

    void OnMouseDown()
    {
        shape.DisplayText();
        if (messageText != null)
        {
            messageText.text = $"You clicked on a {shape.ShapeName}!";
        }
    }
}
