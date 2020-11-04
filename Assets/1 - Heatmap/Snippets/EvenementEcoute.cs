using UnityEngine;
using UnityEditor;
    public class EvenementEcoute
{
    public EvenementEcoute(EvenementsDeclaration ed)
    {
        ed.OnUpdate += Ed_OnUpdate;
    }

    private void Ed_OnUpdate(object sender, EvenementsDeclaration.OnUpdateArgs e)
    {
        Debug.Log("Nouveau x: "+e.x);
    }
}

