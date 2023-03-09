# Domain Models

## Menu
```csharp
{
    class Menu
    {
        Menu Create();
        void AddDinner(Dinner dinner);
        void RemoveDinner(Dinner dinner);
        void UpdateSection(MenuSection dinner);
    }
}

```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "hostId": "00000000-0000-0000-0000-000000000000",
    "createdDateTime": "2020-01-01T00:00:00.0000000Z",
    "updatedDateTime": "2020-01-01T00:00:00.0000000Z",
    "averageRating": 4.5,
    "dinnerIds":
    [
        "00000000-0000-0000-0000-000000000000",
    ],
    "menuReviewIds":
    [
        "00000000-0000-0000-0000-000000000000",
    ],  
    "name" : "My Menu",
    "description" : "My sweet Menu",
    "sections":
    [
        {
            "id": "00000000-0000-0000-0000-000000000000",
            "name": "Soups",
            "description": "Starters",
            "price": 5.99,
             "items":
            [
                {
                    "id": "00000000-0000-0000-0000-000000000000",
                    "name": "Mushroom Soup",
                    "description": "Soured and creamy mushroom soup with bread",
                    "price": "5.99",
                    
                },
            ]
        },
    ]
}
```