using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evenements

{
    // Event Handler
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;

    // Local class definition
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    public Evenements()
    {
        /*
         * Dans notre exemple le sender est la grille
         * Et eventArgs défini plus haut peut prendre les valurs qu'on veut
         * */
        OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) => // La signanture reste tjr la même. 
        {
            int x = eventArgs.x;
            int y = eventArgs.y;
            // Faire des trucs avec les var dans eventArgs
        };
    }

    /* Exemple de méthode qui déclenche l'évènement */
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0)
        {
            int[,] gridArray = new int[1, 1];
            gridArray[x, y] = value;

            if (OnGridValueChanged != null) // Teste si l'handler est bien init
            {
                OnGridValueChanged(this, // Appel de l'handler...
                    new OnGridValueChangedEventArgs { x = x, y = y }  // et on instancie à la volée une classe anonyme qui contient nos paramètres.
                ); 
            }
        }
    }


}
