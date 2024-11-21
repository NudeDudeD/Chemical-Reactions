using System;
using System.Linq;
using UnityEngine;

[Serializable]
public partial class Reaction : IComparable<Reaction>
{
    [Serializable]
    public enum Agent
    {
        Heat,
        Light,
        Electricity
    }

    [Serializable]
    public enum VisualEffect
    {
        Default,
        Explosion
    }

    [SerializeField] private Substance _reactive;
    [SerializeField] private Substance _product;
    [SerializeField] private Substance _additionalReactive;
    [SerializeField] private Substance _additionalProduct;
    [SerializeField] private Agent[] _agents;
    [SerializeField] private VisualEffect _effect;
    [SerializeField] private bool _worksInReverse;

    public string Name
    {
        get
        {
            string reactives = "{" + _reactive.Name + "}" + (_additionalReactive != null ? " + {" + _additionalReactive.Name + "}" : string.Empty);
            string products;
            if (_product != null)
                products = "{" + _product.Name + "}" + (_additionalProduct != null ? " + {" + _additionalProduct.Name + "}" : string.Empty);
            else if (_additionalProduct != null)
                products = "{" + _additionalProduct.Name + "}";
            else
                products = "Nothing";

            return reactives + " " + (_worksInReverse ? "<" : string.Empty) + "=> " + products;
        }
    }

    public Substance Reactive => _reactive;
    public Substance Product => _product;
    public Substance AdditionalReactive => _additionalReactive;
    public Substance AdditionalProduct => _additionalProduct;
    public Agent[] Agents => _agents;
    public VisualEffect Effect => _effect;
    public bool WorksInReverse => _worksInReverse;

    public Reaction(Substance reactive, Substance additionalReactive = null, Substance product = null, Substance additionalProduct = null, Agent[] reactionAgents = null, VisualEffect reactionEffect = VisualEffect.Default, bool worksInReverse = false)
    {
        if (reactive == null && _additionalReactive != null)
            _reactive = _additionalReactive;
        else
        { 
            _reactive = reactive;
            _additionalReactive = additionalReactive;
        }

        if (product == null && additionalProduct != null)
            _product = additionalProduct;
        else
        {
            _product = product;
            _additionalProduct = additionalProduct;
        }

        _agents = reactionAgents;
        _effect = reactionEffect;
        _worksInReverse = worksInReverse;
    }

    private bool CompareSubstancePairs(Substance substance1, Substance substance2, Substance comparable1, Substance comparable2)
    {
        //Debug.Log("{" + substance1?.Name + "} , {" + substance2?.Name + "} : {" + comparable1?.Name + "} , {" + comparable2?.Name + "}");
        return substance1 == comparable1 && substance2 == comparable2 || substance1 == comparable2 && substance2 == comparable1;
    }

    public int CompareTo(Reaction other)
    {
        if (other.WorksInReverse != _worksInReverse || other.Effect != _effect || _agents.Length != other._agents.Length)
            return 0;

        for (int i = 0; i < _agents.Length; i++)
            if (!other._agents.Contains(_agents[i]))
                return 0;

        Substance reactive = other.Reactive;
        Substance product = other.Product;
        Substance additionalReactive = other.AdditionalReactive;
        Substance additionalProduct = other.AdditionalProduct;
        if (CompareSubstancePairs(reactive, additionalReactive, _reactive, _additionalReactive) &&
            CompareSubstancePairs(product, additionalProduct, _product, _additionalProduct) || _worksInReverse && 
            CompareSubstancePairs(reactive, additionalReactive, _product, _additionalProduct) &&
            CompareSubstancePairs(product, additionalProduct, _reactive, _additionalReactive))
            return 1;

        return 0;
    }

    public bool CanReact(Substance reactive, Substance additionalReactive, Agent[] activeAgents, out bool reversed)
    {
        reversed = false; 

        for (int i = 0; i < _agents.Length; i++)
            if (!activeAgents.Contains(_agents[i]))
                return false;

        if (CompareSubstancePairs(reactive, additionalReactive, _reactive, _additionalReactive))
            return true;
        else if (_worksInReverse && CompareSubstancePairs(reactive, additionalReactive, _product, _additionalProduct))
        {
            reversed = true;
            return true;
        }

        return false;
    }

    public bool HasSubstance(Substance substance)
    {
        return substance == _reactive || substance == _additionalReactive || substance == _product || substance == _additionalProduct;
    }
}