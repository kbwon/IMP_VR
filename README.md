# The Other Side

> **VR Horror Puzzle Adventure**  
> 플레이어가 폐저택을 탐사하며 특수 카메라로 보이지 않는 단서와 위험을 발견하고, 방마다 배치된 퍼즐과 추격 이벤트를 해결해 탈출하는 VR 공포 퍼즐 게임입니다.

---

## Repository

- **GitHub Repository**: https://github.com/kbwon/IMP_VR.git
- **Final Branch**: [`develop`](https://github.com/kbwon/IMP_VR/tree/develop)
- **Project Type**: Team Project / VR Horror Puzzle Game
- **Engine**: Unity 6000.0.41f1
- **Main Language**: C#
- **Target Platform**: PC VR / OpenXR-compatible VR environment

> 최종 결과물은 `develop` 브랜치에 정리되어 있습니다.  
> 저장소를 확인할 때는 반드시 `develop` 브랜치를 기준으로 봐 주세요.

---

## Table of Contents

- [Project Overview](#project-overview)
- [Portfolio Summary](#portfolio-summary)
- [Core Gameplay](#core-gameplay)
- [Key Features](#key-features)
- [My Contributions](#my-contributions)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [How to Run](#how-to-run)
- [Gameplay Walkthrough](#gameplay-walkthrough)
- [Team Members](#team-members)
- [External Assets and Tools](#external-assets-and-tools)
- [Notes](#notes)

---

## Project Overview

**The Other Side**는 VR 환경에서 플레이어가 직접 문을 열고, 물건을 집고, 단서를 조사하며 진행하는 1인칭 공포 퍼즐 게임입니다.

플레이어는 어두운 건물 내부를 탐색하며 각 방에 숨겨진 열쇠와 단서를 찾아야 합니다. 일반 시야에서는 보이지 않는 단서와 공간이 존재하며, 이를 확인하기 위해 **특수 카메라**를 사용해야 합니다. 카메라를 통해 숨겨진 숫자, 문, 흔적, 위험 요소를 발견하고, 방마다 다른 퍼즐을 해결하면서 최종적으로 건물에서 탈출하는 것이 목표입니다.

---

## Portfolio Summary

- Unity 기반 VR 게임 제작 경험
- XR Interaction Toolkit을 활용한 VR 상호작용 구현
- 특수 카메라를 이용한 은닉 단서/오브젝트 표시 시스템 구현
- 방 단위 퍼즐, 열쇠, 문, 아이템, 이벤트 흐름 구성
- 몬스터 등장, 추격, 사망 조건 등 공포 게임 이벤트 구현
- 팀 프로젝트에서 기능별 분업 후 통합하는 개발 경험

---

## Core Gameplay

플레이어는 폐저택 내부의 방을 순서대로 탐색하면서 다음 흐름을 반복합니다.

1. 방을 탐색한다.
2. VR 상호작용으로 오브젝트를 집거나 조사한다.
3. 특수 카메라를 사용해 숨겨진 단서나 공간을 찾는다.
4. 단서에 맞는 퍼즐을 해결한다.
5. 열쇠를 획득해 다음 방으로 이동한다.
6. 특정 조건에서 등장하는 몬스터를 피하거나 추격 이벤트에서 생존한다.
7. 최종 열쇠를 얻고 입구로 돌아가 탈출한다.

---

## Key Features

### 1. Special Camera System

특수 카메라는 이 게임의 핵심 장치입니다.

- 카메라 사용 중에만 보이는 숨겨진 단서와 오브젝트 표시
- 카메라 사용 시간 제한 및 HUD 표시
- 카메라 활성화 시 후처리 효과와 터널링 비네트 적용
- 공포 분위기를 강화하기 위한 시야 변화 연출
- 특정 퍼즐, 숨겨진 방, 단서 발견에 사용

관련 구현 요소:

- `CameraController.cs`
- `CameraManager.cs`
- `FollowSocket.cs`
- `HUD` prefab
- 카메라 전용 표시 오브젝트
- 카메라 사용 상태에 따른 오브젝트 활성/비활성 처리

---

### 2. VR Interaction

플레이어가 VR 환경에서 직접 조작하는 상호작용을 구현했습니다.

- 오브젝트 잡기
- 문 손잡이 조작
- 열쇠 획득
- 책/쪽지 확인
- 접시 파편 수집
- 헤드램프 착용
- 특정 위치에 아이템 사용
- 퍼즐 오브젝트 조사

VR 게임에서 중요한 몰입감을 위해 단순 버튼 입력보다 “직접 잡고 확인하는” 상호작용을 중심으로 구성했습니다.

---

### 3. Room-Based Puzzle Flow

게임은 여러 방을 순서대로 탐색하는 구조입니다.

각 방은 서로 다른 퍼즐과 이벤트를 가지고 있습니다.

- Room 1: 특수 카메라와 다음 방 열쇠 획득
- Room 2: 카메라로 숨겨진 숫자 단서 확인
- Room 3: 선반 등반, 접시 파편, 헤드램프 획득
- Room 4: 벽지를 찢어 숨겨진 방 발견, 욕실 퍼즐, 첫 추격 이벤트
- Room 5: 좀비에게 올바른 피자를 만들어 전달하는 퍼즐
- Room 6: Bookhead 몬스터 생존 이벤트 및 최종 열쇠 획득
- Final: 최종 추격 후 탈출

---

### 4. Monster and Chase Events

공포 게임의 긴장감을 만들기 위해 여러 몬스터 이벤트를 구성했습니다.

- 플레이어 시선에 반응하는 Statue
- 특정 조건에서 등장하는 Demon Doll
- 방 안에 오래 머무르면 공격하는 Bookhead Monster
- 방을 나간 뒤 일정 시간 후 복도로 따라오는 추격 이벤트
- 최종 열쇠 획득 후 발생하는 마지막 추격 이벤트
- 플레이어 접촉/조건 실패 시 사망 처리

관련 구현 요소:

- `Statue.cs`
- `MonsterTest.cs`
- `MonsterAI.cs`
- `BookheadMover.cs`
- `FourthRoomManager.cs`
- `SixthRoomManager.cs`
- `LastRun.cs`
- `PlayerInfo.cs`
- `CanEscapeManager.cs`

---

### 5. Environmental Puzzle Objects

방마다 다른 방식의 퍼즐 오브젝트를 사용합니다.

- 열쇠와 문 번호 매칭
- 숨겨진 숫자 단서
- 3×4 서랍 위치 퍼즐
- 깨지는 접시와 접시 파편
- 벽지 제거로 숨겨진 방 발견
- 욕조/화장실 연동 퍼즐
- 피자 재료 조합 퍼즐
- 헤드램프 착용 후 어두운 공간 탐색

관련 구현 요소:

- `Door.cs`
- `Doorkey.cs`
- `BreakableWall.cs`
- `DishOriginal.cs`
- `DishPieceGrabReporter.cs`
- `GetBreakingDish.cs`
- `SheetRackCase.cs`
- `Bath.cs`
- `BathAndToiletManager.cs`
- `MakePizzaAtHere.cs`
- `FeedPizzaAtHere.cs`
- `FinalPizza.cs`

---

### 6. Audio and Horror Feedback

공포 분위기를 위해 상황별 사운드와 이벤트 연출을 사용했습니다.

- 걷기 사운드
- 문 열림/잠김 사운드
- 몬스터 등장 사운드
- 오브젝트별 랜덤 사운드
- 배경음악 및 볼륨 조절
- ElevenLabs를 활용한 일부 상황별 효과음 생성

관련 구현 요소:

- `WalkingSound.cs`
- `DoorSoundManager.cs`
- `Sounds.cs`
- `PlayBackgroundMusic.cs`
- `VolumeControl.cs`
- `AudioSourceBatchSetter.cs`

---

## My Contributions

**김병욱**

이 프로젝트에서 저는 게임 콘셉트 기획과 VR 상호작용, 특수 카메라 시스템, 일부 몬스터 이벤트 구현을 담당했습니다.

### Main Responsibilities

- 게임 콘셉트 및 핵심 플레이 구조 기획
- 특수 카메라 시스템 구현
- 카메라 사용 중 숨겨진 오브젝트/단서 표시 기능 구현
- 카메라 HUD 및 사용 시간 관리
- 카메라 모드 활성화/비활성화 흐름 구현
- 카메라 모드 중 후처리 효과 및 UI 연동
- 플레이어 머리 위치에 장착되는 HeadDrop/HeadLamp 관련 prefab 구성
- 시선 반응형 Statue 몬스터 프로토타입 구현
- 플레이어와 몬스터 충돌 시 사망 처리 프로토타입 구현
- 팀원이 만든 방별 퍼즐/이벤트와 카메라 시스템 연동

### Main Scripts

- `CameraController.cs`
- `CameraManager.cs`
- `FollowSocket.cs`
- `MonsterTest.cs`
- `Statue.cs`

### Main Prefabs

- `HeadDrop`
- `HeadLamp`
- `HUD`

### Implementation Highlights

#### Special Camera

특수 카메라는 단순한 시각 효과가 아니라 퍼즐 진행에 직접 연결되는 핵심 시스템입니다.  
플레이어는 카메라를 사용해야만 보이지 않는 숫자, 숨겨진 문, 특정 단서 등을 확인할 수 있습니다.

카메라 사용 시 다음 기능이 함께 동작합니다.

- HUD 표시
- 배터리/사용 시간 감소
- 숨겨진 오브젝트 표시
- 후처리 효과 활성화
- 카메라 비활성화 시 상태 복구

#### Statue Monster

Statue는 플레이어의 시선과 거리에 반응하는 몬스터입니다.  
플레이어가 바라보는 동안에는 눈을 뜨고 플레이어를 마주 보지만, 일정 조건을 만족하지 못하면 사망 이벤트로 이어집니다.

이를 통해 VR 환경에서 시선 자체를 게임플레이 요소로 활용하는 공포 연출을 실험했습니다.

---

## Tech Stack

| Category | Technology |
|---|---|
| Engine | Unity 6000.0.41f1 |
| Language | C# |
| Rendering | Universal Render Pipeline |
| VR Framework | XR Interaction Toolkit |
| XR Runtime | OpenXR |
| Input | Unity Input System |
| Animation | Unity Animation / Animation Rigging |
| Navigation | Unity AI Navigation |
| Audio | Unity Audio System, Audio Mixer |
| AI Sound Tool | ElevenLabs |
| External Assets | Unity Asset Store, Mixamo, CGTrader, Sketchfab, Pixabay |
