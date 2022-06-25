using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Functions;

public class FeatureRepository
{
    private List<Feature> _features;

    public FeatureRepository()
    {
        _features = new List<Feature>
        {
            {
                new Feature
                {
                    Id = 7,
                    Status = FeatureStatus.Open
                }
            },
            {
                new Feature
                {
                    Id = 8,
                    Status = FeatureStatus.Closed
                }
            },
            {
                new Feature
                {
                    Id = 9,
                    Status = FeatureStatus.Open
                }
            },
            {
                new Feature
                {
                    Id = 21,
                    Status = FeatureStatus.Open
                }
            },
        };
    }
    public IEnumerable<Feature> GetOpenFeatures()
    {
        return _features.Where(feature => feature.Status == FeatureStatus.Open);
    }
}