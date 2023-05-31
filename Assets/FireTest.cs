using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class FireTest : MonoBehaviour
{
    FirebaseFirestore db;

    // Start is called before the first frame update
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
    }

    // Update is called once per frame
    public void Test()
    {
        int id = Random.Range(0, 100);

        DocumentReference docRef = db.Collection("cities").Document(id.ToString());
        Dictionary<string, object> city = new Dictionary<string, object>
        {
                { "Id", id },
                { "Name", "Los Angeles" },
                { "State", "CA" },
                { "Country", "USA" }
        };
        docRef.SetAsync(city).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the LA document in the cities collection.");
        });
    }
}
