using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public interface ICard
    {
        Guid GetId();

        void OnStart();

        void OnUpdate();

        void OnExit();
    }

    public abstract class Card : ICard
    {
        private readonly Guid Id = new Guid();

        public Guid GetId()
        {
            return Id;
        }

        public virtual void OnStart()
        {
            //print pudim
           //do something does 
        }

        public virtual void OnUpdate()
        {
            throw new NotImplementedException();
        }

        public virtual void OnExit()
        {
            throw new NotImplementedException();
        }

    }

    //ja vem implementado as coisas da de cima /\
    public class CardA : Card
    {
        public override void OnStart()
        {
         //   faz algo diferente no onStart
            
          //base.OnStart();//vai chamar o printn pudim //caso queira chamar o metodo antes declarado 

        }


    }


    public class MainFSMCard : MonoBehaviour
    {
        private List<ICard> _currentsCardExtremamenteGenerica = new List<ICard>();

        public Guid PlayACard(ICard cardExtremamenteGenerica)
        {
          cardExtremamenteGenerica.OnStart();
            _currentsCardExtremamenteGenerica.Add(cardExtremamenteGenerica);
            return cardExtremamenteGenerica.GetId();
        }

        public void RemovingACard(Guid Id)
        {

            var cardToRemove = _currentsCardExtremamenteGenerica.FirstOrDefault(card => card.GetId() == Id);
            cardToRemove.OnExit();

            _currentsCardExtremamenteGenerica.Remove(cardToRemove);
        }

        void Update()
        {
            foreach (var card in _currentsCardExtremamenteGenerica)
            {
                card.OnUpdate();
            }
        }
    }
















    //se por acaso voce nao quer estender as coisas de Card usa assim
    public abstract class TrapNoARegularCard : ICard
    {
        public void OnStart()
        {
            //faz algo diferente do padrao
        }

        public void OnUpdate()
        {
            throw new NotImplementedException();
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }
    }



}
