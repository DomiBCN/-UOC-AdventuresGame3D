﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZombieState {

    void UpdateState();
    void GoToAttackState();
    void GoToAlertState();
    void GoToPatrolState();
    void OnTriggerEnter(Collider col);
    void OnTriggerStay(Collider col);
    void OnTriggerExit(Collider col);
}
