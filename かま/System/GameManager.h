#pragma once

// �p�����override����֐��͕K��virtual������
// �g�b�v�ƂȂ�N���X�����N���X�A�X�[�p�[�N���X�Ƃ���
class Object
{
protected:
	// �s��
	KdMatrix          m_mWorld;

	// �I�u�W�F�N�g���ۑ��p
	std::string m_Name = "No Name";

public:

	std::string GetName() { return m_Name; }
	void SetName(const std::string _name) { m_Name = _name; }

	// ���z�f�X�g���N�^(����R�����g�͏����Ȃ�����)
	virtual ~Object(){}

	// �X�V����
	virtual void Update(){}

	// �`�揈��
	virtual void Draw(){}

	// �s����Z�b�g
	void SetMatrix(const KdMatrix& m) { m_mWorld = m; }

	// ImGui�̍��ڂ�`��
	virtual void ImGuiUpdate();

	/*==============================*/
	// �f�V���A���C�Y / �V���A���C�Y
	/*==============================*/
	virtual void Deserialize(const json11::Json& jsonObj);

	// �����̃f�[�^��outJson�֊i�[����
	virtual void Serialize(json11::Json::object& outJson);
};


/*================================*/
//  �Q�[���̎���ƂȂ镔��
/*================================*/

class GameManager
{
private:
	// �I�u�W�F�N�g���X�g
	// �L�����N�^�������Ƃ��͕K���|�C���^��
	std::list<SPtr<Object>> m_ObjectList;

	/*=========================*/
	//  Editor(���ۂ̐��쎞�͕ʂ̂Ƃ���ɗp�ӂ��Ă�������)
	/*=========================*/

	WPtr<Object>   m_Editor_SelectObj;

	// �J�����œ�����
	
	EdiorCamera m_Editor_Camera;
public:
	// �����ݒ�
	void Init();

	// �Q�[���I�u�W�F�N�g�ǉ��֐�
	void AddObject(SPtr<Object> _obj) { m_ObjectList.push_back(_obj); }

	// �Q�[������
	// �Q�[���̃��C���֐��ɋ߂��̂ōX�V�ƕ`��������Ă���
	void Run();

private :
	// ImGui�̏���
	void ImGuiDraw();

};