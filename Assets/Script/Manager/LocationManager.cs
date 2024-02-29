using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEditor.FilePathAttribute;

[System.Serializable]
public class Location
{
    public string locationName;

    public Location parentLocation;
    public Dictionary<Location, Status> childLocation = new Dictionary<Location, Status>(); //자식 지역 및 가중치 정보
}

public class LocationManager : MonoBehaviour
{
    private static LocationManager instance;
    public static LocationManager Instance
    {
        get
        {
            if (instance == null)
                instance = new LocationManager();

            return instance;
        }
    }

    public Location topLocation;
    public Location currentLocation;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        TestLogic1();
    }

    public void TestLogic1()
    {
        topLocation = new Location
        {
            locationName = "마을",
        };

        Location child1 = new Location
        {
            locationName = "시장",
        };

        Location child1child1 = new Location
        {
            locationName = "무기 상점",
        };

        Location child1child2 = new Location
        {
            locationName = "잡화점",
        };

        Location child1child3 = new Location
        {
            locationName = "방어구 상점",
        };

        Location child2 = new Location
        {
            locationName = "광장",
        };

        Location child2child = new Location
        {
            locationName = "여관",
        };

        Location child1childchild1 = new Location
        {
            locationName = "방",
        };

        Location child1childchild2 = new Location
        {
            locationName = "식당",
        };

        // 마을 -> 시장
        topLocation.childLocation.Add(child1, new Status
        {
            hp = 0,
            fatigue = -5,
            hungry = -5,
            money = 0,
            time = 7,
        });

        // 시장 -> 무기 상점
        child1.childLocation.Add(child1child1, new Status
        {
            hp = 0,
            fatigue = -5,
            hungry = -5,
            money = 0,
            time = 7,
        });

        // 시장 -> 잡화점
        child1.childLocation.Add(child1child2, new Status
        {
            hp = 0,
            fatigue = -5,
            hungry = -5,
            money = 0,
            time = 7,
        });

        // 시장 -> 방어구 상점
        child1.childLocation.Add(child1child3, new Status
        {
            hp = 0,
            fatigue = -5,
            hungry = -5,
            money = 0,
            time = 7,
        });

        // 마을 -> 광장
        topLocation.childLocation.Add(child2, new Status
        {
            hp = 0,
            fatigue = -10,
            hungry = -10,
            money = 0,
            time = 15,
        });

        //광장-> 여관
        child1.childLocation.Add(child2child, new Status
        {
            hp = 0,
            fatigue = -8,
            hungry = -8,
            money = 0,
            time = 9,
        });
        // 여관 -> 방
        child2child.childLocation.Add(child1childchild1, new Status
        {
            hp = 0,
            fatigue = -3,
            hungry = -3,
            money = 0,
            time = 2,
        });
        // 여관 -> 식당
        child2child.childLocation.Add(child1childchild2, new Status
        {
            hp = 0,
            fatigue = -4,
            hungry = -4,
            money = 0,
            time = 3,
        });

        child1.parentLocation = topLocation;    //광장 부모 -> 마을
        child2.parentLocation = topLocation;    //시장 부모 -> 마을
        child1child1.parentLocation = child1;
        child1child2.parentLocation = child1;
        child1child3.parentLocation = child1;

        child2child.parentLocation = child1;    //여관 부모 -> 광장
        child1childchild1.parentLocation = child2child; //여관 부모 방
        child1childchild2.parentLocation = child2child; //여관 부모 식당

        // JSON으로 직렬화
        // 테스트 로직
        //Debug.Log(Application.dataPath);
        string json = JsonUtility.ToJson(topLocation);
        string filePath = Path.Combine(Application.dataPath, "location.json");
        File.WriteAllText(filePath, json);
    }

    private void Start()
    {
        //대충 여기서 json을 다 파싱하고 난 후 behavior을 생성하면 되지않을까?
        currentLocation = FindLocation(Player.Instance, topLocation);

        if (currentLocation == null)
            Debug.LogError("현재 플레이어 위치를 찾을 수 없음");
    }

    public Location FindLocation(Player player, Location location)
    {
        Location returnLocation = null;

        if (location.childLocation != null)
        {
            foreach (var childLocation in location.childLocation.Keys)
            {
                returnLocation = FindLocation(player, childLocation);

                if (returnLocation != null)
                    return returnLocation;
            }
        }

        if (location.locationName == player.CurrentLocation)
            return location;

        return returnLocation;
    }

    int bFirstToParent = 0;
    public Status CalculateTotalStatus(string startLocationName, string endLocationName)
    {
        Location startLocation = FindLocationByName(startLocationName, topLocation);
        Location endLocation = FindLocationByName(endLocationName, topLocation);

        Status totalStatus = new Status();

        if (startLocation != null && endLocation != null)
        {
            // Find the path and calculate total status
            List<Location> path = FindShortestPath(startLocation, endLocation, bFirstToParent);

            string a = "";

            foreach (Location location in path)
            {
                a += location.locationName + "->";
                if (location != null && location.parentLocation != null)
                {
                    Status locationStatus = location.parentLocation.childLocation[location];
                    totalStatus.hp += locationStatus.hp;
                    totalStatus.fatigue += locationStatus.fatigue;
                    totalStatus.hungry += locationStatus.hungry;
                    totalStatus.time += locationStatus.time;
                }
            }
            Debug.Log(a);
        }

        return totalStatus;
    }

    public List<Location> FindShortestPath(Location startLocation, Location endLocation, int bFirstToParent)
    {
        Dictionary<Location, float> distance = new Dictionary<Location, float>();
        Dictionary<Location, Location> previous = new Dictionary<Location, Location>();
        HashSet<Location> visitedLocations = new HashSet<Location>();
        List<Location> path = new List<Location>();

        foreach (var location in GetAllLocations(topLocation))
        {
            distance[location] = float.MaxValue;
            previous[location] = null;
        }

        distance[startLocation] = 0;

        while (true)
        {
            Location currentLocation = null;
            float minDistance = float.MaxValue;

            foreach (var location in distance.Keys)
            {
                if (!visitedLocations.Contains(location) && distance[location] < minDistance)
                {
                    minDistance = distance[location];
                    currentLocation = location;
                }
            }

            if (currentLocation == null)
                break;

            visitedLocations.Add(currentLocation);

            foreach (var connectedLocation in GetAllConnectedLocations(currentLocation))
            {
                float alt = distance[currentLocation] + GetEdgeWeight(currentLocation, connectedLocation);
                if (alt < distance[connectedLocation])
                {
                    distance[connectedLocation] = alt;
                    previous[connectedLocation] = currentLocation;
                }
            }
        }

        Location traceLocation = endLocation;
        while (traceLocation != null)
        {
            path.Insert(0, traceLocation);
            traceLocation = previous[traceLocation];
        }

        return path;
    }

    private IEnumerable<Location> GetAllConnectedLocations(Location location)
    {
        List<Location> connectedLocations = new List<Location>();

        foreach (var childLocation in location.childLocation.Keys)
        {
            connectedLocations.Add(childLocation);
        }

        if (location.parentLocation != null)
        {
            connectedLocations.Add(location.parentLocation);
        }

        return connectedLocations;
    }

    private IEnumerable<Location> GetAllLocations(Location location)
    {
        List<Location> locations = new List<Location> { location };

        foreach (var childLocation in location.childLocation.Keys)
        {
            locations.AddRange(GetAllLocations(childLocation));
        }

        return locations;
    }

    private float GetEdgeWeight(Location startLocation, Location endLocation)
    {
        if (startLocation.childLocation.TryGetValue(endLocation, out var status))
        {
            return status.time;
        }

        return int.MaxValue; // 연결되어 있지 않은 경우 무한대로 설정
    }

    public Location FindLocationByName(string locationName, Location currentLocation)
    {
        if (currentLocation.locationName == locationName)
            return currentLocation;

        if (currentLocation.childLocation != null)
        {
            foreach (var childLocation in currentLocation.childLocation.Keys)
            {
                Location result = FindLocationByName(locationName, childLocation);
                if (result != null)
                    return result;
            }
        }

        return null;
    }

}
