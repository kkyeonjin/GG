# 01 Team GG Game Project : 🍰 A Piece of Quake!
## <프로젝트 소개>
### 지진 대응 수칙 홍보를 위한 퍼즐 & 레이싱 게임
![ezgif com-video-to-gif](https://github.com/kkyeonjin/GG/assets/88366217/7491b30c-efd2-4ca1-be9c-9ad617882696)
- 장르 : 1인칭 3D, 방탈출, 장애물 레이싱, 배틀로얄
- 플랫폼 :	PC
- 개발 엔진 :	Unity
- 개발 목적	: 다양한 상황 및 장소별 지진 대응 수칙의 홍보 및 학습
- 타겟 :	지진 대응 수칙이 익숙하지 않은 남녀노소 누구나

 
## <멤버 구성>
- 홍혜담(1971098) : 서버, 게임 시스템 전반
- 우송은(1976239) : 지하철 스테이지, AI 학습
- 박현진(2071025) : 집 스테이지, UI 디자인


## <주요 기능>
### 1. 집 / 지하철 - 서로 다른 두 가지 환경의 게임 스테이지
   - 혼자 있는 상황, 군중과 함께 있는 상황을 모두 체험 가능하도록 두 개의 스테이지로 게임을 구성함
   - 대응 수칙을 요소화한 퍼즐을 맵 내에 구현해 게임을 플레이하며 대응 수칙을 체험할 수 있도록 함

### 2. 대응 수칙 수집 및 열람 시스템
   - 게임 플레이 중 퍼즐을 풀거나 특정 행동을 취할 경우 그에 관련된 대응 수칙이 팝업 및 해금됨
   - 플레이 종료 후에도 별도의 열람 시스템으로 수집한 대응 수칙을 모아볼 수 있도록 함

### 3. 지하철 스테이지의 군중 AI npc
   - 공공장소인 지하철 맵의 현실감을 높이기 위한 군중 AI npc를 배치함
   - 지진 대피 시 질서 유지 수칙을 고려해 AI npc와 충돌 시 질서 게이지가 감소함

### 4. 퍼즐 요소 및 멀티 플레이 지원
   - 방탈출이 메인 장르인 집 맵에는 퍼즐 요소, 레이싱이 메인 장르인 지하철 맵에는 멀티 플레이 지원을 통해 게임적 재미 요소를 부여함

### 5. 보상 및 상점 기능
   - 각 맵 클리어 시 소요 시간에 따른 보상(골드, 경험치)를 제공함
   - 보상으로 상점에서 아바타/아이템을 구매할 수 있도록 해 재미 요소 및 동기부여, 반복적 접속을 유도함

    
## <게임 구성도>
![구성도](https://github.com/kkyeonjin/GG/assets/88366217/b42c91b7-ee33-4eb6-8331-b5950f1e55d7)

### How to build
 프로젝트 파일 다운로드 후 Unity(2021.3.24f)에서 GG폴더 열기 
 → Unity Editor에서 왼쪽 상단 File 메뉴
 → Build Settings
   * Platform : Windows, Mac, Linux
   * Target Platform : Windows, Architecture Intel 64-bit
 → Build 

### How to install
별도 설치 방법 없음. 

### How to test
빌드 후 생성되는 .exe 파일 실행
이후 게임 플레이 테스트 방법은 제품설명서 - 조작법, 인게임 화면설명 시트 참고.

## 게임 실행 파일 (.exe)
## 제품 설명서

## 참고한 오픈소스코드
[Earthquake-Simulator](https://github.com/Habrador/Earthquake-Simulator)

[dungeon-generator](https://github.com/silverlybee/dungeon-generator)
