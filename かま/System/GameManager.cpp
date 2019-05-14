#include "main.h"


#include "GameManager.h"

SPtr<Object> CreateObject(const std::string& className);

void Object::ImGuiUpdate()
{
	// �{���͂����ɏ����Ă͂����Ȃ�
	ImGuiInputString("Name", m_Name);

	// ��R���� : �h���b�O�����Ƃ��̑��x��ݒ�
	//ImGui::DragFloat3("Pos", m_mWorld.GetPos(), 0.01f);
	ImGuizmoEditTransform(m_mWorld, ShMgr.m_mView, ShMgr.m_mProj, nullptr);
}

void Object::Deserialize(const json11::Json& jsonObj)
{
	m_Name = jsonObj["Name"].string_value();

	if (jsonObj["Pos"].is_array()) {    // ���΂炭����ɂ��Ă���(���������)
		auto& pos = jsonObj["Pos"].array_items();
		//KdMatrix m;
		m_mWorld.CreateMove(
			(float)pos[0].number_value(),
			(float)pos[1].number_value(),
			(float)pos[2].number_value()
		);
	}

	m_mWorld.Deserialize(jsonObj["Matrix"]);
}

void Object::Serialize(json11::Json::object& outJson)
{
	// ���O
	outJson["Name"] = m_Name;

	// �s��
	json11::Json::array mat;
    mat.resize(16);
	outJson["Matrix"] = m_mWorld.Serialize();;


}

void GameManager::Init()
{
	// JSON�p�ɓǂݍ���
	// JSON�t�@�C���̓��e��S�ĕ�����Ƃ��ēǂݍ��ށB 
	std::string str = KdLoadStringFile("data/StageData.json");
	// JSON��� 
	std::string err;
	json11::Json jsn = json11::Json::parse(str, err);
	if (err.size() > 0) {
		// ��̓G���[ err�ɃG���[���e��������œ����Ă��� 
	}

	auto& ary = jsn["Objects"].array_items();
	for (auto&& jsonObj : ary) {
		auto& className = jsonObj["ClassName"].string_value();

		// �I�u�W�F�N�g��錾
		SPtr<Object> p;
		p = CreateObject(className);

		// �f�V���A���C�Y
		p->Deserialize(jsonObj);

		AddObject(p);

		/*
		if (className == "StageObject") {
		// �X�e�[�W
		SPtr<StageObject> p = std::make_shared<StageObject>();
		p->SetModel(jsonObj["ModelFileName"].string_value());

		auto& pos = jsonObj["Pos"].array_items();
		KdMatrix m;
		m.CreateMove(
		(float)pos[0].number_value(),
		(float)pos[1].number_value(),
		(float)pos[2].number_value()
		);

		p->SetMatrix(m);

		p->SetName(jsonObj["Name"].string_value());

		gameManager.AddObject(p);
		}
		else if (className == "Chara") {
		// �L����
		SPtr<Chara> c = std::make_shared<Chara>();
		c->SetModel(jsonObj["ModelFileName"].string_value());

		auto& pos = jsonObj["Pos"].array_items();
		KdMatrix m;
		m.CreateMove(
		(float)pos[0].number_value(),
		(float)pos[1].number_value(),
		(float)pos[2].number_value()
		);

		c->SetMatrix(m);

		c->SetHp(jsonObj["Hp"].int_value());

		c->SetName(jsonObj["Name"].string_value());

		gameManager.AddObject(c);
		}
		*/
	}
}

void GameManager::Run()
{
	//===================================================
	// �X�V����
	//===================================================

	for (auto&& obj : m_ObjectList) {
		obj->Update();
	}

	/*=============================================*/
	// EditorCamera����
	/*=============================================*/
	m_Editor_Camera.Update();

	//===================================================
	// �`�揈��
	//===================================================

	// �J����
	// �r���[�s��
	/*ShMgr.m_mView.CreateMove(0, 1, -5);
	ShMgr.m_mView.Inverse();

	// �ˉe�s��
	ShMgr.m_mProj.CreatePerspectiveFovLH(60.0f, 1.0f, 0.01f, 1000.0f);

	// �J���������V�F�[�_�փZ�b�g
	ShMgr.UpdateCamera();
	*/

	// �J�������Z�b�g
	m_Editor_Camera.SetToShader();

	// �o�b�N�o�b�t�@���N���A(RGBA�̏�)
	KD3D.GetBackBuffer()->ClearRT({ 0,0,1,1 });

	// Z�o�b�t�@���N���A
	KD3D.GetZBuffer()->ClearDepth();

	
	// �I�u�W�F�N�g��`��
	for (auto&& obj : m_ObjectList) {
		obj->Draw();
	}

	// ImGui�J�n  
	ImGui_ImplDX11_NewFrame();
	ImGui_ImplWin32_NewFrame();
	ImGui::NewFrame();

	// ImGuizmo
	ImGuizmo::BeginFrame();

	// ImGui Demo �E�B���h�E�\�� ���������Q�l�ɂȂ�E�B���h�E�ł��B
	// imgui_demo.cpp�Q�ƁB  
	ImGui::ShowDemoWindow(nullptr);

	ImGuiDraw();

	// GUI�`����s  
	ImGui::Render();
	ImGui_ImplDX11_RenderDrawData(ImGui::GetDrawData());


	// ���e��\��
	KD3D.GetSwapChain()->Present(0, 0);

}

void GameManager::ImGuiDraw()
{

	/*================================*/
	//  Object�E�B���h�E�쐬
	/*================================*/

	// �E�B���h�E�J�n �gWindowName�h�̕����͑��̃E�B���h�E�Ɣ��Ȃ��悤�ɂ��邱�� 
	if (ImGui::Begin("Object List", nullptr)) {
		

		for (auto& obj : m_ObjectList) {
			// obj�̃A�h���X������ID�Ƃ��ė��p����
			ImGui::PushID((int)obj.get());

			bool b_Selected = false;
			if (m_Editor_SelectObj.lock() == obj)b_Selected = true;

			// ���ݑI������Ă���I�u�W�F�N�g
			if (ImGui::Selectable(obj->GetName().c_str(), b_Selected)) {
				m_Editor_SelectObj = obj;
			}

			// �E�N���b�N�|�b�v�A�b�v���j���[
			if (ImGui::BeginPopupContextItem("Object PopUp Menu", 1)) {

				// Prefab��
				if (ImGui::Selectable("Save to Json File")) {
					// ���̃I�u�W�F�N�g���V���A���C�Y
					json11::Json::object serial;
					obj->Serialize(serial);

					// Json�f�[�^�𕶎���ɕϊ�
					json11::Json jsn = serial;
					KdSaveStringFile("test.json", jsn.dump());
				}

				// Prefab��
				if (ImGui::Selectable("Load from Json File")) {
					std::string strJson = KdLoadStringFile("test.json");
					// ��������͎����ōl����(�����Ă���͓̂����ł͂Ȃ�)
					// JSON��� 
					std::string err;
					json11::Json jsn = json11::Json::parse(strJson, err);
					if (err.size() > 0) {
						// ��̓G���[ err�ɃG���[���e��������œ����Ă��� 
					}

					auto& ary = jsn["Objects"].array_items();
					for (auto&& jsonObj : ary) {
						auto& className = jsonObj["ClassName"].string_value();

						// �I�u�W�F�N�g��錾
						SPtr<Object> p;
						p = CreateObject(className);

						// �f�V���A���C�Y
						p->Deserialize(jsonObj);

						AddObject(p);
					}
				}

				if (ImGui::Selectable("Duplicate")) {
					// ���̃I�u�W�F�N�g���V���A���C�Y
					json11::Json::object serial;
					obj->Serialize(serial);

					// �V����Object�쐬(�{���̓_��)
					std::string className = serial["ClassName"].string_value();
					SPtr<Object> newObj = CreateObject(className);

					newObj->Deserialize(serial);

					AddObject(newObj);
				}
				
				// �K���Ō�ɌĂԂ���
				ImGui::EndPopup();
			}

			ImGui::PopID();
		}

	}
	ImGui::End();

	if (ImGui::Begin("Inspector", nullptr)) {
	    SPtr<Object> select = m_Editor_SelectObj.lock();

		// �����������邩�ǂ����m�F
		if (select) {
			select->ImGuiUpdate();
		}

	}

	ImGui::End();

	/*==================================================*/
	// �����ɕ`�悵�������̂���������
	/*==================================================*/

	//// shift-jis�̕����� 
	//std::string str = "���{��ł��I";
	//// STL��filesystem���g���ĕϊ� 
	//ImGui::Button(filesystem::path(str).u8string().c_str());

	/*==================================================*/

	
}