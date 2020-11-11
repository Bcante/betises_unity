using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


/*
 * Notre classe "type" de la quelle on aura plusieurs instances qu'on voudra comparer
 * Par exemple, plein de noeuds et je veux récupérer le noeud le plus à gauche 
 */
public class Node
{
    public int x;
    public int y;

    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

/*
 * Mon comparateur perso: Il va me permettre de comparer automatiquement les noeuds entre eux.
 * De base je ne peux pas comparer deux noeuds car le compilateur ne saura pas comment les comparer, alors
 * que pour un type primitif comme un int il saura comment.
 **/
public class Comparer : IComparer<Node> // Notre classe, l'important c'est de préciser entre <> quel type d'objet on souhaite comparer
{
    /*
     * La méthode à implémenter sinon ça compile pô. On renvoie un entier
     * On renvoie un entier :
     * <1 : le premier objet est inférieur au second
     * 0 : égalité
     * >1 : Le deuxième objet est supérieur au second
     * 
     * Cela permettera de savoir comment positionner les objets dans la liste plus tard
     * */
    public int Compare(Node n1, Node n2)
    {
        if (n1.x > n2.x)
        {
            return 1;
        }
        else if (n1.x == n2.x)
        {
            return 0;
        }
        else
        {
            return -1;
        }
        // Note: on aurait pu abréger en return n1.x.CompareTo(n2.x);
    }
}

public class SortedListSnippet : MonoBehaviour
{
    

    private void Start()
    {
        /*
     * Pour tester on crée 3 noeuds qu'on insère dans le désordre. 
     * Ensuite on regarde leur valeur en faisant un Debug.Log sur tous ces éléments dans l'ordre pour vérifier 
     * qu'ils sont triés sur leur X.
     * */

        Node n1 = new Node(1, 1);
        Node n2 = new Node(2, 1);
        Node n3 = new Node(3, 1);

        SortedList<Node, int> list = new SortedList<Node, int>(new Comparer());  // On crée notre comparateur et on précise que la méthode de comparation est celle définie au dessus
        /* Insertion dans le désordre... Les -1 ne servent à rien, mais on a pas de Sorted List sans <Key Value> :/*/
        list[n2] = -1;
        list[n3] = -1;
        list[n1] = -1;

        
        foreach (Node n in list.Keys)
        {
            Debug.Log(n.x); //1 2 3
        }
    }

    private void Update()
    {
        
    }
}





