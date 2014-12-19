
public interface ICollidable {


    void VisitCollision(CollisionGoal cg);

    void VisitCollision(CollisionLine cl);

    void Accept(ICollidable visitor);
}
