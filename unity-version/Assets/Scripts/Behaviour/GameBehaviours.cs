using UnityEngine;
using System.Collections;

public class GameBehaviours : MonoBehaviour {
    private void Update()
    {
        if (
            SceneRoot.Instance.SceneState.State == StateBase.ESubState.Default)
        {
            GameUpdate();
            GameArbiter.Instance.ProcessTurnEvents();
        }

    }

    protected virtual void Start()
    {

    }

    /// <summary>
    /// Fixeds the update.
    /// </summary>
    private void FixedUpdate()
    {
        if (
            SceneRoot.Instance.SceneState.State == StateBase.ESubState.Default)
        {
            GameFixedUpdate();
           // GameArbiter.Instance.ProcessTurnEvents();
        }
    }
    private void LateUpdate()
    {
        if (
            SceneRoot.Instance.SceneState.State == StateBase.ESubState.Default)
        {
            GameLateUpdate();
            GameArbiter.Instance.ProcessTurnEvents();
        }

    }

    /// <summary>
    /// Games the update.
    /// </summary>
    protected virtual void GameUpdate()
    {

    }

    /// <summary>
    /// Games the update.
    /// </summary>
    protected virtual void GameFixedUpdate()
    {

    }
    protected virtual void GameLateUpdate()
    {

    }



    #region Finds Method

    /// <summary>
    /// Finds the in tree.
    /// </summary>
    /// <returns>The in tree.</returns>
    /// <param name="name">Name.</param>
    protected virtual GameObject FindComponentInTree(string name)
    {

        Transform root = transform.root;

        Component[] allTransform = root.GetComponentsInChildren<Transform>(true);

        foreach (Transform t in allTransform)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }

        Debug.LogWarning("Can't find " + name);

        return null;

    }

    /// <summary>
    /// Finds the in tree.
    /// </summary>
    /// <returns>The in tree.</returns>
    /// <param name="type">Type.</param>
    /// <param name="name">Name.</param>
    protected virtual Component FindComponentInTree(System.Type type, string name)
    {

        GameObject go = FindComponentInTree(name);

        Component result = null;
        if (go != null)
        {
            result = go.GetComponent(type) as Component;
        }

        if (result == null)
        {
            Debug.LogWarning("No attached component " + type);
        }

        return result;

    }


    /// <summary>
    /// Finds the in tree.
    /// </summary>
    /// <returns>The in tree.</returns>
    /// <param name="type">Type.</param>
    /// <param name="name">Name.</param>
    protected virtual Component[] FindComponentsInTree(System.Type type, string name)
    {

        GameObject go = FindComponentInTree(name);

        Component[] result = null;
        if (go != null)
        {
            result = go.GetComponents(type) as Component[];
        }

        if (result == null)
        {
            Debug.LogWarning("No attached component " + type);
        }

        return result;

    }

    /// <summary>
    /// Finds the components in parent.
    /// </summary>
    /// <returns>The components in parent.</returns>
    /// <param name="type">Type.</param>
    /// <param name="name">Name.</param>
    protected virtual Component[] FindComponentsInParent(System.Type type, string name)
    {

        GameObject go = transform.parent.gameObject;

        Component[] result = null;
        if (go != null)
        {
            result = go.GetComponents(type) as Component[];
        }

        if (result == null)
        {
            Debug.LogWarning("No attached component " + type);
        }

        return result;

    }

    /// <summary>
    /// Finds the in tree.
    /// </summary>
    /// <returns>The in tree.</returns>
    /// <param name="name">Name.</param>
    public static GameObject UtilFindInTree(GameObject go, string name)
    {
        Transform root = go.transform;
        if (go.transform.root != null)
        {
            root = go.transform.root;
        }

        Component[] allTransform = root.GetComponentsInChildren<Transform>(true);

        foreach (Transform t in allTransform)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }

        Debug.LogWarning("Can't find " + name);

        return null;
    }
    /// <summary>
    /// Finds the in tree.
    /// </summary>
    /// <returns>The in tree.</returns>
    /// <param name="type">Type.</param>
    /// <param name="name">Name.</param>
    public static Component UtilFindInTree(GameObject searchGo, System.Type type, string name)
    {
        GameObject go = UtilFindInTree(searchGo, name);

        Component result = null;
        if (go != null)
        {
            result = go.GetComponent(type) as Component;
        }

        if (result == null)
        {
            Debug.LogWarning("No attached component " + type);
        }

        return result;
    }


    #endregion



}
